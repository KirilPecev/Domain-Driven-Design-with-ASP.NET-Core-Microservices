namespace CarRentalSystem.Application.Features.Dealers
{
    using Contracts;
    using Domain.Models.Dealers;
    using Queries.Details;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken);

        Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken);
    }
}
