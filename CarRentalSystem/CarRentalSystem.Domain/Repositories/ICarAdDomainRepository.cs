namespace CarRentalSystem.Domain.Repositories
{
    using Common;
    using Models.CarAds;

    public interface ICarAdDomainRepository : IDomainRepository<CarAd>
    {
        Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken);

        Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken);

        Task<CarAd> Find(int id, CancellationToken cancellationToken);

        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
