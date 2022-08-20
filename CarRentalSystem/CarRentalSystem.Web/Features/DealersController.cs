using CarRentalSystem.Application.Features.Dealers.Queries.Details;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Web.Features
{
    public class DealersController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<DealerDetailsOutputModel>> Details([FromRoute] DealerDetailsQuery query)
            => await this.Send(query);
    }
}
