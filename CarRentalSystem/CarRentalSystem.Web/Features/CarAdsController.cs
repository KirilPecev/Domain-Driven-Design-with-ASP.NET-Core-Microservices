﻿namespace CarRentalSystem.Web.Features
{
    using Application.Features;
    using Application.Features.CarAds.ChangeAvailability;
    using Application.Features.CarAds.Commands.Create;
    using Application.Features.CarAds.Commands.Edit;
    using Application.Features.CarAds.Queries.Categories;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Mine;
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

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine([FromQuery] MineCarAdsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<ActionResult<IEnumerable<GetCarAdCategoryOutputModel>>> Categories([FromQuery] GetCarAdCategoriesQuery query)
            => await this.Send(query);

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id, EditCarAdCommand command)
            => await this.Send(command.SetId(id));

        [HttpPut]
        [Authorize]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        public async Task<ActionResult> ChangeAvailability([FromRoute] ChangeAvailabilityCommand command)
            => await this.Send(command);
    }
}
