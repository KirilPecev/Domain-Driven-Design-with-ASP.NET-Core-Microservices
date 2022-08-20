namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Features.Dealers;
    using AutoMapper;
    using CarRentalSystem.Application.Features.Dealers.Queries.Details;
    using CarRentalSystem.Domain.Exceptions;
    using Domain.Models.Dealers;

    using Microsoft.EntityFrameworkCore;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        private readonly IMapper mapper;

        public DealerRepository(CarRentalDbContext db, IMapper mapper) : base(db)
            => this.mapper = mapper;

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

        public async Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<DealerDetailsOutputModel>(this
                    .All()
                    .Where(d => d.Id == id))
                .FirstOrDefaultAsync(cancellationToken);
    }
}
