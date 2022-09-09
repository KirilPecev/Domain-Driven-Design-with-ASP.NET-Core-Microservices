namespace CarRentalSystem.Web.Features
{
    using Application.Features;
    using Application.Features.CarAds.ChangeAvailability;
    using Application.Features.CarAds.Commands.Create;
    using Application.Features.CarAds.Commands.Delete;
    using Application.Features.CarAds.Commands.Edit;
    using Application.Features.CarAds.Queries.Categories;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Mine;
    using Application.Features.CarAds.Queries.Search;

    using CarRentalSystem.Application.Features.Identity.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CarAdsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search([FromQuery] SearchCarAdsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(CreateCarAdCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CarAdDetailsOutputModel>> Details([FromRoute] CarAdDetailsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine([FromQuery] MineCarAdsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<ActionResult<IEnumerable<GetCarAdCategoryOutputModel>>> Categories([FromQuery] GetCarAdCategoriesQuery query)
            => await this.Send(query);

        [HttpPut]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id, EditCarAdCommand command)
            => await this.Send(command.SetId(id));

        [HttpPut]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        public async Task<ActionResult> ChangeAvailability([FromRoute] ChangeAvailabilityCommand command)
            => await this.Send(command);

        [HttpDelete]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        [Route(Id)]
        public async Task<ActionResult> Delete([FromRoute] DeleteCarAdCommand command)
            => await this.Send(command);
    }
}
