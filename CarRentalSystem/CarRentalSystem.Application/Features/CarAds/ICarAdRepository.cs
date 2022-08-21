namespace CarRentalSystem.Application.Features.CarAds
{
    using Contracts;
    using Domain.Models.CarAds;
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
    }
}
