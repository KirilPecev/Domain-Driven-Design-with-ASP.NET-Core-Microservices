namespace CarRentalSystem.Domain
{
    using Common;

    using Dealerships.Models.CarAds;

    using Microsoft.Extensions.DependencyInjection;

    using Statistics.Models;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IFactory<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime())
            .AddTransient<IInitialData, CategoryData>()
            .AddTransient<IInitialData, StatisticData>();
    }
}
