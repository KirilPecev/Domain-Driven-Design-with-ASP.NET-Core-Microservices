namespace CarRentalSystem.Application.Features.CarAds
{
    using CarAds.Common;
    using CarRentalSystem.Application.Features.CarAds.Queries.Categories;
    using Contracts;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Specifications;
    using Queries.Details;
    using Queries.Search;

    public interface ICarAdRepository : IRepository<CarAd>
    {
        Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
             Specification<CarAd> specification,
             CancellationToken cancellationToken = default);

        Task<int> Total(CancellationToken cancellationToken = default);

        Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken);

        Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken);

        Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken);
        Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(CancellationToken cancellationToken);
        Task<int> Total(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CancellationToken cancellationToken);

        Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CarAdsSortOrder searchOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default);
    }
}
