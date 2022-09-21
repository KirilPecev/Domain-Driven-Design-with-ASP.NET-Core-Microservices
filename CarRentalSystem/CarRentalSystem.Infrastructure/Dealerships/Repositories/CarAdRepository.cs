namespace CarRentalSystem.Infrastructure.Dealerships.Repositories
{
    using System.Linq;

    using Application.Common.Contracts;
    using Application.Dealerships.CarAds;
    using Application.Dealerships.CarAds.Queries.Categories;
    using Application.Dealerships.CarAds.Queries.Common;
    using Application.Dealerships.CarAds.Queries.Details;

    using AutoMapper;

    using Common;
    using Common.Persistence;

    using Domain.Common;
    using Domain.Dealerships.Models.CarAds;
    using Domain.Dealerships.Models.Dealers;
    using Domain.Dealerships.Repositories;

    using Microsoft.EntityFrameworkCore;

    internal class CarAdRepository : DataRepository<IDealershipDbContext, CarAd>,
        ICarAdDomainRepository,
        ICarAdQueryRepository
    {
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;
        private readonly string cacheKey = $"{typeof(CarAd)}";

        public CarAdRepository(CarRentalDbContext db, IMapper mapper, ICacheService cacheService)
            : base(db)
        {
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            CarAd? carAd = await Data.CarAds.FindAsync(id);

            if (carAd == null)
            {
                return false;
            }

            Data.CarAds.Remove(carAd);

            await Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<CarAd> Find(int id, CancellationToken cancellationToken)
            => await
                All()
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(CancellationToken cancellationToken)
        {
            Dictionary<int, GetCarAdCategoryOutputModel> categories = await mapper
               .ProjectTo<GetCarAdCategoryOutputModel>(Data.Categories)
               .ToDictionaryAsync(k => k.Id, cancellationToken);

            var carAdsPerCategory = await AllAvailable()
                .GroupBy(c => c.Category.Id)
                .Select(gr => new
                {
                    CategoryId = gr.Key,
                    TotalCarAds = gr.Count()
                })
                .ToListAsync(cancellationToken);

            carAdsPerCategory.ForEach(c => categories[c.CategoryId].TotalCarAds = c.TotalCarAds);

            return categories.Values;
        }

        public async Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CarAdsSortOrder searchOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default)
            => (await mapper
                .ProjectTo<TOutputModel>(
                    GetCarAdsQuery(carAdSpecification, dealerSpecification)
                    .Sort(searchOrder))
                .ToListAsync(cancellationToken))
                .Skip(skip)
                .Take(take);

        public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken)
            => await
                Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        public async Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken)
            => await mapper
                .ProjectTo<CarAdDetailsOutputModel>(
                    AllAvailable()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken)
            => await
                Data
                .Manufacturers
                .FirstOrDefaultAsync(m => m.Name == manufacturer, cancellationToken);

        public async Task<int> Total(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CancellationToken cancellationToken)
            => await
                GetCarAdsQuery(carAdSpecification, dealerSpecification)
                .CountAsync(cancellationToken);

        private IQueryable<CarAd> AllAvailable()
           =>
               All()
               .Where(car => car.IsAvailable);

        private IQueryable<CarAd> GetCarAdsQuery(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification)
        {
            bool hasValue = cacheService.TryGet(cacheKey, out IReadOnlyList<Dealer> cachedDealers);
            if (!hasValue)
            {
                cachedDealers = Data
                    .Dealers
                    .Include(d => d.CarAds)
                    .ToList();

                cacheService.Set(cacheKey, cachedDealers);
            }

            return cachedDealers
                .AsQueryable()
                .Where(dealerSpecification)
                .SelectMany(d => d.CarAds)
                .Where(carAdSpecification);
        }
    }
}
