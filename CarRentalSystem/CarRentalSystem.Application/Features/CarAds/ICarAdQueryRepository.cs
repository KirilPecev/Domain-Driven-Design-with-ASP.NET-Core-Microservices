namespace CarRentalSystem.Application.Features.CarAds
{
    using Common;
    using Contracts;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Specifications;
    using Queries.Categories;
    using Queries.Details;

    public interface ICarAdQueryRepository : IQueryRepository<CarAd>
    {
        Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
           Specification<CarAd> carAdSpecification,
           Specification<Dealer> dealerSpecification,
           CarAdsSortOrder searchOrder,
           int skip = 0,
           int take = int.MaxValue,
           CancellationToken cancellationToken = default);

        Task<CarAdDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCarAdCategories(CancellationToken cancellationToken = default);

        Task<int> Total(
            Specification<CarAd> carAdSpecification,
            Specification<Dealer> dealerSpecification,
            CancellationToken cancellationToken = default);
    }
}
