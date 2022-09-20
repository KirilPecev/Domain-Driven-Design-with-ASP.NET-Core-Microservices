namespace CarRentalSystem.Application.Features.Dealers
{
    using Contracts;
    using Dealers.Queries.Common;
    using Dealers.Queries.Details;
    using Domain.Models.Dealers;

    public interface IDealerQueryRepository : IQueryRepository<Dealer>
    {
        Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<DealerOutputModel> GetDetailsByCarId(int id, CancellationToken cancellationToken = default);
    }
}
