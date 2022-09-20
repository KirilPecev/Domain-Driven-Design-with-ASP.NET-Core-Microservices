namespace CarRentalSystem.Domain.Repositories
{
    using Common;
    using Domain.Models.Dealers;

    public interface IDealerDomainRepository : IDomainRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken = default);

        Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default);
    }
}
