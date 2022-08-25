namespace CarRentalSystem.Application.Features.CarAds.Queries.Mine
{
    using CarAds.Common;

    public class MineCarAdsOutputModel : CarAdsOutputModel<MineCarAdOutputModel>
    {
        public MineCarAdsOutputModel(
            IEnumerable<MineCarAdOutputModel> carAds,
            int page,
            int totalPages) : base(carAds, page, totalPages) { }
    }
}
