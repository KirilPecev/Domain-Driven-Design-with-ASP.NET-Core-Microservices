namespace CarRentalSystem.Domain.Statistics.Models.Statistics
{
    using Common.Models;

    public class CarAdView : Entity<int>
    {
        internal CarAdView(int carAdId, string? userId)
        {
            CarAdId = carAdId;
            UserId = userId;
        }

        public int CarAdId { get; }

        public string? UserId { get; }
    }
}
