namespace CarRentalSystem.Application.Features.CarAds.Queries.Search
{
    using CarAds.Common;

    public class SearchCarAdsOutputModel : CarAdsOutputModel<CarAdOutputModel>
    {
        public SearchCarAdsOutputModel(
            IEnumerable<CarAdOutputModel> carAds,
            int page,
            int totalPages) : base(carAds, page, totalPages) { }
    }
}
