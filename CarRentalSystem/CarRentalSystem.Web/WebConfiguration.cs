namespace CarRentalSystem.Web
{
    using Application.Common;
    using Application.Contracts;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddValidatorsFromAssemblyContaining<Result>()
                .AddControllers()
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
