﻿namespace CarRentalSystem.Infrastructure
{
    using System.Reflection;
    using System.Text;

    using CarRentalSystem.Messages;
    using CarRentalSystem.Services.Messages;

    using Hangfire;
    using Hangfire.SqlServer;

    using MassTransit;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using Models;

    using Services.Identity;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebService<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
            where TDbContext : DbContext
        {
            services
                .AddDatabase<TDbContext>(configuration)
                .AddApplicationSettings(configuration)
                .AddTokenAuthentication(configuration)
                .AddHealth(configuration, databaseHealthChecks, messagingHealthChecks)
                .AddAutoMapperProfile(Assembly.GetCallingAssembly())
                .AddControllers();

            return services;
        }

        public static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
            => services
                .AddScoped<DbContext, TDbContext>()
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetDefaultConnectionString(),
                        sqlOptions => sqlOptions
                            .EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)));

        public static IServiceCollection AddApplicationSettings(
            this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    config => config.BindNonPublicProperties = true);

        public static IServiceCollection AddAutoMapperProfile(
            this IServiceCollection services, Assembly assembly)
            => services
                .AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());

        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services, IConfiguration configuration, JwtBearerEvents events = null)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>();

            string secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            byte[] key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    if (events != null)
                    {
                        bearer.Events = events;
                    }
                });

            return services;
        }

        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration,
            bool usePolling = true,
            bool addDbMessages = true,
            params Type[] consumers)
        {
            if (addDbMessages)
            {
                services
                    .AddTransient<IPublisher, Publisher>()
                    .AddTransient<IMessageService, MessageService>();
            }

            MessageQueueSettings messageQueueSettings = GetMessageQueueSettings(configuration);

            services
                .AddMassTransit(mt =>
                {
                    mt.AddConsumers(consumers);

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(messageQueueSettings.Host, host =>
                        {
                            host.Username(messageQueueSettings.Username);
                            host.Password(messageQueueSettings.Password);
                        });

                        consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, consumer);
                        }));

                    }));
                });

            if (usePolling)
            {
                CreateHangfireDatabase(configuration);

                services
                    .AddHangfire(config => config
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(
                            configuration.GetCronJobsConnectionString(),
                            new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }));

                services.AddHangfireServer();

                services.AddHostedService<MessagesHostedService>();
            }

            return services;
        }

        public static IServiceCollection AddHealth(
           this IServiceCollection services,
           IConfiguration configuration,
           bool databaseHealthChecks = true,
           bool messagingHealthChecks = true)
        {
            IHealthChecksBuilder healthChecks = services.AddHealthChecks();

            if (databaseHealthChecks)
            {
                healthChecks
                    .AddSqlServer(configuration.GetDefaultConnectionString());
            }

            if (messagingHealthChecks)
            {
                MessageQueueSettings messageQueueSettings = GetMessageQueueSettings(configuration);

                string messageQueueConnectionString =
                    $"amqp://{messageQueueSettings.Username}:{messageQueueSettings.Password}@{messageQueueSettings.Host}/";

                healthChecks
                    .AddRabbitMQ(rabbitConnectionString: messageQueueConnectionString);
            }

            return services;
        }

        private static MessageQueueSettings GetMessageQueueSettings(IConfiguration configuration)
        {
            IConfigurationSection settings = configuration.GetSection(nameof(MessageQueueSettings));

            return new MessageQueueSettings(
                settings.GetValue<string>(nameof(MessageQueueSettings.Host)),
                settings.GetValue<string>(nameof(MessageQueueSettings.Username)),
                settings.GetValue<string>(nameof(MessageQueueSettings.Password)));
        }

        private static void CreateHangfireDatabase(IConfiguration configuration)
        {
            string connectionString = configuration.GetCronJobsConnectionString();

            string dbName = connectionString
                .Split(";")[1]
                .Split("=")[1];

            using SqlConnection connection = new SqlConnection(connectionString.Replace(dbName, "master"));

            connection.Open();

            using SqlCommand command = new SqlCommand(
                $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') create database [{dbName}];",
                connection);

            command.ExecuteNonQuery();
        }
    }
}
