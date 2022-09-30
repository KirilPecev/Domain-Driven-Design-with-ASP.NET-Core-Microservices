namespace CarRentalSystem.Identity.Infrastructure
{
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Infrastructure.Identity;
    using CarRentalSystem.Services.Data;

    using Data;
    using Data.Models;

    using Services.Identity;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .ConfigureSettings(configuration)
                .AddWebService<IdentityDbContext>(configuration)
                .AddUserStorage()
                .AddTransient<IDataSeeder, IdentityDataSeeder>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IJwtTokenGeneratorService, JwtTokenGeneratorService>();

        public static IServiceCollection AddUserStorage(this IServiceCollection services)
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
                    .AddEntityFrameworkStores<IdentityDbContext>();

            return services;
        }

        private static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<IdentitySettings>(
                    configuration.GetSection(nameof(IdentitySettings)),
                    config => config.BindNonPublicProperties = true);
    }
}
