namespace CarRentalSystem.Statistics.Infrastructure
{
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Services.Data;

    using Data;

    using Messages;

    using Services.CarAdViews;
    using Services.Statistics;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddWebService<StatisticsDbContext>(configuration)
                .AddTransient<IDataSeeder, StatisticsDataSeeder>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddTransient<ICarAdViewService, CarAdViewService>()
                .AddMessaging(configuration, consumers: typeof(CarAdCreatedConsumer));
    }
}
