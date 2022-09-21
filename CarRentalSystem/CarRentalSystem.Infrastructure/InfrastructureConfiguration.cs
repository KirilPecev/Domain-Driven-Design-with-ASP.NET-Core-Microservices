namespace CarRentalSystem.Infrastructure
{
    using System.Text;

    using Application;
    using Application.Contracts;
    using Application.Features.Identity;

    using CarRentalSystem.Application.Common;
    using CarRentalSystem.Application.Common.Contracts;
    using CarRentalSystem.Application.Identity;

    using Configuration;

    using Domain.Common;

    using Identity;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using Persistence;

    public static class InfrastructureConfiguration
    {
        private const string CacheConfiguration = "CacheConfiguration";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddRepositories()
                .AddIdentity(configuration)
                .AddCache(configuration);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<CarRentalDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(CarRentalDbContext)
                            .Assembly.FullName)))
                .AddTransient<IInitializer, CarRentalDbInitializer>();

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
           => services
               .Scan(scan => scan
                   .FromCallingAssembly()
                   .AddClasses(classes => classes
                       .AssignableTo(typeof(IDomainRepository<>))
                       .AssignableTo(typeof(IQueryRepository<>)))
                   .AsMatchingInterface()
                   .WithTransientLifetime());

        private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<CarRentalDbContext>();

            string secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            byte[] key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                });

            services.AddTransient<IIdentity, IdentityService>();

            return services;
        }

        private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<CacheConfiguration>(configuration.GetSection(CacheConfiguration))
                .AddMemoryCache()
                .AddTransient<ICacheService, MemoryCacheService>();
    }
}
