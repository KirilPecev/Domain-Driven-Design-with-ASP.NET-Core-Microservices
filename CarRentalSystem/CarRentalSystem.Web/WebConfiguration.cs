namespace CarRentalSystem.Web
{
    using Application.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Web.Services;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }
    }
}
