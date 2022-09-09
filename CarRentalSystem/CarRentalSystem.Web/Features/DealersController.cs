﻿namespace CarRentalSystem.Web.Features
{
    using Application.Features;
    using Application.Features.Dealers.Commands.Edit;
    using Application.Features.Dealers.Queries.Details;

    using CarRentalSystem.Application.Features.Identity.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealersController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<DealerDetailsOutputModel>> Details([FromRoute] DealerDetailsQuery query)
            => await this.Send(query);

        [HttpPut]
        [Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.User}")]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id, EditDealerCommand command)
            => await this.Send(command.SetId(id));
    }
}
