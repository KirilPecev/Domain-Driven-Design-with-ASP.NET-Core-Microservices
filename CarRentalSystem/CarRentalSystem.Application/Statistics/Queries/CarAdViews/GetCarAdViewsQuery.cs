namespace CarRentalSystem.Application.Statistics.Queries.CarAdViews
{
    using MediatR;

    using Statistics;

    public class GetCarAdViewsQuery : IRequest<int>
    {
        public int CarAdId { get; set; }

        public class GetCarAdViewsQueryHandler : IRequestHandler<GetCarAdViewsQuery, int>
        {
            private readonly IStatisticsQueryRepository statistics;

            public GetCarAdViewsQueryHandler(IStatisticsQueryRepository statistics)
                => this.statistics = statistics;

            public async Task<int> Handle(
                GetCarAdViewsQuery request,
                CancellationToken cancellationToken)
                => await statistics.GetCarAdViews(request.CarAdId, cancellationToken);
        }
    }
}
