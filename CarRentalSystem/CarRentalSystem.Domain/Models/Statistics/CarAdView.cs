namespace CarRentalSystem.Domain.Models.Statistics
{
    using Common;

    public class CarAdView : Entity<int>
    {
        internal CarAdView(int carAdId, string? userId)
        {
            this.CarAdId = carAdId;
            this.UserId = userId;
        }

        public int CarAdId { get; }

        public string? UserId { get; }
    }
}
