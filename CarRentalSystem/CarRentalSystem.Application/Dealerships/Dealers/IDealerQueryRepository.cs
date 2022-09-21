namespace CarRentalSystem.Application.Dealerships.Dealers
{
    using Application.Common.Contracts;

    using Domain.Models.Dealers;

    using Queries.Common;
    using Queries.Details;

    public interface IDealerQueryRepository : IQueryRepository<Dealer>
    {
        Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<DealerOutputModel> GetDetailsByCarId(int id, CancellationToken cancellationToken = default);
    }
}
