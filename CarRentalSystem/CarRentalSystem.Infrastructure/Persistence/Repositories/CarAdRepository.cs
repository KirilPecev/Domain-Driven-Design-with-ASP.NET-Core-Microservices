﻿namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;

    using Application.Common;
    using Application.Contracts;
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
        private readonly ICacheService cacheService;
        private readonly string cacheKey = $"{typeof(CarAd)}";

        public CarAdRepository(CarRentalDbContext db, IMapper mapper, ICacheService cacheService)
            : base(db)
        {
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<Result> Delete(int id, CancellationToken cancellationToken)
        {
            CarAd? carAd = await this.Data.CarAds.FindAsync(id);

            if (carAd == null)
            {
                return false;
            }

            this.Data.CarAds.Remove(carAd);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<CarAd> Find(int id, CancellationToken cancellationToken)
            => await this
                .All()
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

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
        {
            bool hasValue = this.cacheService.TryGet(this.cacheKey, out IReadOnlyList<Dealer> cachedDealers);
            if (!hasValue)
            {
                List<Dealer> dealers = this.Data
                    .Dealers
                    .Include(d => d.CarAds)
                    .ToList();

                this.cacheService.Set(this.cacheKey, dealers);
            }

            return cachedDealers
                .AsQueryable()
                .Where(dealerSpecification)
                .SelectMany(d => d.CarAds)
                .Where(carAdSpecification);
        }
    }
}
