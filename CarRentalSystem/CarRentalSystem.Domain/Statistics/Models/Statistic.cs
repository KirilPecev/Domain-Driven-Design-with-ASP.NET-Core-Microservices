namespace CarRentalSystem.Domain.Statistics.Models
{
    using Common;

    public class Statistic : IAggregateRoot
    {
        private readonly HashSet<CarAdView> carAdViews;

        internal Statistic()
        {
            this.TotalCarAds = 0;
            this.carAdViews = new HashSet<CarAdView>();
        }

        public int TotalCarAds { get; private set; }

        public IReadOnlyCollection<CarAdView> CarAdViews
            => this.carAdViews.ToList().AsReadOnly();

        public void AddCarAd() => this.TotalCarAds++;

        public void AddCarAdView(int carAdId, string? userId)
            => this.carAdViews.Add(new CarAdView(carAdId, userId));
    }
}
