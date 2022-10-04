namespace CarRentalSystem.Dealers.Infrastructure
{
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Services.Data;

    using Data;

    using Services.CarAds;
    using Services.Categories;
    using Services.Dealers;
    using Services.Manufacturers;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddWebService<DealersDbContext>(configuration)
                .AddTransient<IDataSeeder, DealersDataSeeder>()
                .AddTransient<IDealerService, DealerService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<ICarAdService, CarAdService>()
                .AddTransient<IManufacturerService, ManufacturerService>()
                .AddMessaging(configuration);
    }
}
