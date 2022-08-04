namespace CarRentalSystem.Application.Features.Dealers
{
    using Contracts;
    using Domain.Models.Dealers;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken);
    }
}
