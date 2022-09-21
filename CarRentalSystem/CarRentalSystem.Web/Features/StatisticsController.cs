namespace CarRentalSystem.Web.Features
{
    using Application.Features.Statistics.Queries.CarAdViews;
    using Application.Features.Statistics.Queries.Current;

    using CarRentalSystem.Application.Statistics.Queries.CarAdViews;
    using CarRentalSystem.Application.Statistics.Queries.Current;

    using Microsoft.AspNetCore.Mvc;

    public class StatisticsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<int>> Get([FromQuery] GetCarAdViewsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(Details))]
        public async Task<ActionResult<GetCurrentStatisticsOutputModel>> Details([FromQuery] GetCurrentStatisticsQuery query)
           => await this.Send(query);
    }
}
