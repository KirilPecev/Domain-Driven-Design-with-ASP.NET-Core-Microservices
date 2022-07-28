namespace CarRentalSystem.Web.Features
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class CarAdsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<int> Get() => Enumerable.Range(1, 10).ToArray();
    }
}
