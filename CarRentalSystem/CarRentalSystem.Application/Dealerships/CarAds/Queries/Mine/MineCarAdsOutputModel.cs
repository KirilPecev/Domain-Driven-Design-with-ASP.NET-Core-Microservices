namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Mine
{
    using Common;

    public class MineCarAdsOutputModel : CarAdsOutputModel<MineCarAdOutputModel>
    {
        public MineCarAdsOutputModel(
            IEnumerable<MineCarAdOutputModel> carAds,
            int page,
            int totalPages) : base(carAds, page, totalPages) { }
    }
}
