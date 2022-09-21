namespace CarRentalSystem.Application.Common
{
    public class CacheSettings
    {
        public int AbsoluteExpirationInHours { get; set; }

        public int SlidingExpirationInMinutes { get; set; }
    }
}