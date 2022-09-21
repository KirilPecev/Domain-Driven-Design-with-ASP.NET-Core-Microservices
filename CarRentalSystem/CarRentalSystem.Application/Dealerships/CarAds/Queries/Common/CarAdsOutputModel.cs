namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Common
{
    public abstract class CarAdsOutputModel<TCarAdOutputModel>
    {
        internal CarAdsOutputModel(
            IEnumerable<TCarAdOutputModel> carAds,
            int page,
            int totalPages)
        {
            CarAds = carAds;
            Page = page;
            TotalPages = totalPages;
        }

        public IEnumerable<TCarAdOutputModel> CarAds { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}
