namespace CarRentalSystem.Domain.Dealerships.Repositories
{
    using Common;

    using Models.Dealers;

    public interface IDealerDomainRepository : IDomainRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken = default);

        Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default);
    }
}
