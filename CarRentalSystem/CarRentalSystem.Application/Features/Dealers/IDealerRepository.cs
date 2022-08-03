namespace CarRentalSystem.Application.Features.Dealers
{
    using Application.Contracts;
    using Domain.Models.Dealers;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task Save(Dealer dealer, CancellationToken cancellationToken = default);
    }
}
