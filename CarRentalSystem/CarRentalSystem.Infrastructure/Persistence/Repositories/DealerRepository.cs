namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Features.Dealers;
    using CarRentalSystem.Domain.Exceptions;
    using Domain.Models.Dealers;

    using Microsoft.EntityFrameworkCore;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(CarRentalDbContext db) : base(db) { }

        public async Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken)
        {
            Dealer? dealer = await this
                .Data
                .Users
                .Where(u => u.Id == userId)
                .Select(u => u.Dealer)
                .FirstOrDefaultAsync(cancellationToken);

            if (dealer == null)
            {
                throw new InvalidDealerException("This user is not a dealer.");
            }

            return dealer;
        }
    }
}
