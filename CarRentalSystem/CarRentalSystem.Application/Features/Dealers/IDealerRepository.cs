namespace CarRentalSystem.Application.Features.Dealers
{
    using Contracts;
    using Domain.Models.Dealers;
    using Queries.Common;
    using Queries.Details;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken);

        Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken);

        Task<DealerOutputModel> GetDetailsByCarId(int id, CancellationToken cancellationToken);

        Task<int> GetDealerId(string userId, CancellationToken cancellationToken);

        Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken);
    }
}
