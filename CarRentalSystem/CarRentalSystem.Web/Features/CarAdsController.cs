namespace CarRentalSystem.Web.Features
{
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class CarAdsController : ControllerBase
    {
        private static readonly Dealer Dealer = new Dealer("Dealer", "+12345678");

        [HttpGet]
        public IEnumerable<CarAd> Get() => Dealer.CarAds.Where(c => c.IsAvailable);
    }
}
