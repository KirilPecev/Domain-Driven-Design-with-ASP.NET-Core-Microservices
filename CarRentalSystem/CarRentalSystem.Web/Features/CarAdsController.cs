namespace CarRentalSystem.Web.Features
{
    using Application.Features.CarAds.Commands.Create;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Search;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CarAdsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search([FromQuery] SearchCarAdsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(CreateCarAdCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CarAdDetailsOutputModel>> Details([FromRoute] CarAdDetailsQuery query)
            => await this.Send(query);
    }
}
