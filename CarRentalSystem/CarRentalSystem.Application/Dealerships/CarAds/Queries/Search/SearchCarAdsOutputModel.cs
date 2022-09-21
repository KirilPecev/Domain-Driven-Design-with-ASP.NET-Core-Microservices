namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Search
{
    using Common;

    public class SearchCarAdsOutputModel : CarAdsOutputModel<CarAdOutputModel>
    {
        public SearchCarAdsOutputModel(
            IEnumerable<CarAdOutputModel> carAds,
            int page,
            int totalPages) : base(carAds, page, totalPages) { }
    }
}
