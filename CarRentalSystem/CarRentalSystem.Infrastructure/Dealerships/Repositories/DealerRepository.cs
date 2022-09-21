namespace CarRentalSystem.Infrastructure.Dealerships.Repositories
{
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Dealerships.Dealers;
    using Application.Dealerships.Dealers.Queries.Common;
    using Application.Dealerships.Dealers.Queries.Details;

    using AutoMapper;

    using Common.Persistence;

    using Domain.Dealerships.Exceptions;
    using Domain.Dealerships.Models.Dealers;
    using Domain.Dealerships.Repositories;

    using Identity;

    using Microsoft.EntityFrameworkCore;

    internal class DealerRepository : DataRepository<IDealershipDbContext, Dealer>,
        IDealerDomainRepository,
        IDealerQueryRepository
    {
        private readonly IMapper mapper;

        public DealerRepository(CarRentalDbContext db, IMapper mapper) : base(db)
            => this.mapper = mapper;

        public Task<int> GetDealerId(
           string userId,
           CancellationToken cancellationToken = default)
           => FindByUser(userId, user => user.Dealer!.Id, cancellationToken);

        public Task<Dealer> FindByUser(
            string userId,
            CancellationToken cancellationToken = default)
            => FindByUser(userId, user => user.Dealer!, cancellationToken);

        public async Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
            => await mapper
                .ProjectTo<DealerDetailsOutputModel>(
                    All()
                    .Where(d => d.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<DealerOutputModel> GetDetailsByCarId(int id, CancellationToken cancellationToken = default)
            => await mapper
                .ProjectTo<DealerOutputModel>(
                    All()
                    .Where(d => d.CarAds.Any(ad => ad.Id == id)))
                .SingleOrDefaultAsync(cancellationToken);

        private async Task<T> FindByUser<T>(
            string userId,
            Expression<Func<User, T>> selector,
            CancellationToken cancellationToken = default)
        {
            var dealerData = await
                Data
                .Users
                .Where(u => u.Id == userId)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);

            if (dealerData == null)
            {
                throw new InvalidDealerException("This user is not a dealer.");
            }

            return dealerData;
        }

        public async Task<bool> HasCarAd(int dealerId, int carAdId, CancellationToken cancellationToken)
            => await
                All()
                .Where(d => d.Id == dealerId)
                .AnyAsync(d => d.CarAds
                    .Any(c => c.Id == carAdId));
    }
}
