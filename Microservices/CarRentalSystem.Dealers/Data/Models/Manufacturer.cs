namespace CarRentalSystem.Dealers.Data.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CarAd> CarAds { get; set; } = new List<CarAd>();
    }
}
