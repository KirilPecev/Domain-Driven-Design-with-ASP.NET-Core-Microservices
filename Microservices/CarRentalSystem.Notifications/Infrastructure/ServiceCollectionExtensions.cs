namespace CarRentalSystem.Identity.Infrastructure
{
    using CarRentalSystem.Infrastructure;

    using Notifications.Infrastructure;
    using Notifications.Messages;

    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddCors()
                .AddTokenAuthentication(configuration, JwtConfiguration.BearerEvents)
                .AddHealth(configuration, databaseHealthChecks: false)
                .AddMessaging(configuration, usePolling: false, addDbMessages: false, consumers: typeof(CarAdCreatedConsumer))
                .AddSignalR();
    }
}
