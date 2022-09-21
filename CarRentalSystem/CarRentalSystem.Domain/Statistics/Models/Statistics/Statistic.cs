namespace CarRentalSystem.Domain.Statistics.Models.Statistics
{
    using Common;

    public class Statistic : IAggregateRoot
    {
        private readonly HashSet<CarAdView> carAdViews;

        internal Statistic()
        {
            TotalCarAds = 0;
            carAdViews = new HashSet<CarAdView>();
        }

        public int TotalCarAds { get; private set; }

        public IReadOnlyCollection<CarAdView> CarAdViews
            => carAdViews.ToList().AsReadOnly();

        public void AddCarAd() => TotalCarAds++;

        public void AddCarAdView(int carAdId, string? userId)
            => carAdViews.Add(new CarAdView(carAdId, userId));
    }
}
