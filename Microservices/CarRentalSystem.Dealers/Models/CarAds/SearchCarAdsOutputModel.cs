namespace CarRentalSystem.Dealers.Models.CarAds
{
    public class SearchCarAdsOutputModel : CarAdsOutputModel<CarAdOutputModel>
    {
        public SearchCarAdsOutputModel(
            IEnumerable<CarAdOutputModel> carAds,
            int page,
            int totalCarAds)
            : base(carAds, page, totalCarAds)
        {
        }
    }
}
