namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Features.Dealers;
    using Domain.Models.Dealers;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(CarRentalDbContext db) : base(db) { }

        public async Task Save(Dealer dealer, CancellationToken cancellationToken = default)
        {
            this.Data.Add(dealer);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
