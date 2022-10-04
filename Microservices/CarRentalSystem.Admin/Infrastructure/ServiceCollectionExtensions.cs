namespace CarRentalSystem.Admin.Infrastructure
{
    using System;
    using System.Reflection;

    using CarRentalSystem.Admin.Services.Dealers;
    using CarRentalSystem.Admin.Services.Identity;
    using CarRentalSystem.Admin.Services.Statistics;
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Services.Identity;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Refit;

    using Services;

    public static class ServiceCollectionExtensions
    {
        private static ServiceEndpoints serviceEndpoints;

        public static IServiceCollection AddExternalService<TService>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TService : class
        {
            if (serviceEndpoints == null)
            {
                serviceEndpoints = configuration
                    .GetSection(nameof(ServiceEndpoints))
                    .Get<ServiceEndpoints>(config => config
                        .BindNonPublicProperties = true);
            }

            string serviceName = typeof(TService)
                .Name.Substring(1)
                .Replace("Service", string.Empty);

            services
                .AddRefitClient<TService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceEndpoints[serviceName]));

            return services;
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceEndpoints serviceEndpoints = configuration
               .GetSection(nameof(ServiceEndpoints))
               .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
                .AddTokenAuthentication(configuration)
                .AddHealth(
                    configuration,
                    databaseHealthChecks: false,
                    messagingHealthChecks: false)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services
                .AddRefitClient<IStatisticsService>()
                .WithConfiguration(serviceEndpoints.Statistics);

            services
                .AddRefitClient<IDealersService>()
                .WithConfiguration(serviceEndpoints.Dealers);
        }
    }
}
