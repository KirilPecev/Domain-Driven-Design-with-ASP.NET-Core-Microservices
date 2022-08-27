namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using Application.Features.CarAds;
    using Application.Features.CarAds.Common;
    using Application.Features.CarAds.Queries.Categories;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Search;
    using AutoMapper;
    using Common;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Specifications;

    using Microsoft.EntityFrameworkCore;

    internal class CarAdRepository : DataRepository<CarAd>, ICarAdRepository
    {
        private readonly IMapper mapper;

        public CarAdRepository(CarRentalDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(CancellationToken cancellationToken)
        {
            Dictionary<int, GetCarAdCategoryOutputModel> categories = await this.mapper
               .ProjectTo<GetCarAdCategoryOutputModel>(this.Data.Categories)
               .ToDictionaryAsync(k => k.Id, cancellationToken);

            var carAdsPerCategory = await this.AllAvailable()
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

        public async Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
            Specification<CarAd> specification,
            CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<CarAdListingModel>(this
                    .AllAvailable()
                    .Where(specification))
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CarAdsSortOrder searchOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default)
            => (await this.mapper
                .ProjectTo<TOutputModel>(this
                    .GetCarAdsQuery(carAdSpecification, dealerSpecification)
                    .Sort(searchOrder))
                .ToListAsync(cancellationToken))
                .Skip(skip)
                .Take(take);

        public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken)
            => await this
                .Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        public async Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<CarAdDetailsOutputModel>(this
                    .AllAvailable()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken)
            => await this
                .Data
                .Manufacturers
                .FirstOrDefaultAsync(m => m.Name == manufacturer, cancellationToken);

        public async Task<int> Total(CancellationToken cancellationToken = default)
            => await this
                .AllAvailable()
                .CountAsync(cancellationToken);

        public async Task<int> Total(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CancellationToken cancellationToken)
            => await this
                .GetCarAdsQuery(carAdSpecification, dealerSpecification)
                .CountAsync(cancellationToken);

        private IQueryable<CarAd> AllAvailable()
           => this
               .All()
               .Where(car => car.IsAvailable);

        private IQueryable<CarAd> GetCarAdsQuery(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification)
            => this
                .Data
                .Dealers
                .Where(dealerSpecification)
                .SelectMany(d => d.CarAds)
                .Where(carAdSpecification);
    }
}
