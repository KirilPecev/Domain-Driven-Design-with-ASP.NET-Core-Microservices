namespace CarRentalSystem.Web.Features
{
    using Application.Contracts;
    using Domain.Models.CarAds;

    using Microsoft.AspNetCore.Mvc;

    public class CarAdsController : ApiController
    {
        private readonly IRepository<CarAd> carAds;

        public CarAdsController(IRepository<CarAd> carAds) => this.carAds = carAds;

        [HttpGet]
        public IEnumerable<CarAd> Get() => this.carAds.All().Where(c => c.IsAvailable);
    }
}
