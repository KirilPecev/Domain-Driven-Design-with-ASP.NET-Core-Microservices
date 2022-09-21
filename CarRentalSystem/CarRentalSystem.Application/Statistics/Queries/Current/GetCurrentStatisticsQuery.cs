namespace CarRentalSystem.Application.Statistics.Queries.Current
{
    using MediatR;

    public class GetCurrentStatisticsQuery : IRequest<GetCurrentStatisticsOutputModel>
    {
        public class GetCurrentStatisticsQueryHandler : IRequestHandler<GetCurrentStatisticsQuery, GetCurrentStatisticsOutputModel>
        {
            private readonly IStatisticsQueryRepository statistics;

            public GetCurrentStatisticsQueryHandler(IStatisticsQueryRepository statistics)
                => this.statistics = statistics;

            public async Task<GetCurrentStatisticsOutputModel> Handle(
                GetCurrentStatisticsQuery request,
                CancellationToken cancellationToken)
                => await this.statistics.GetCurrent(cancellationToken);
        }
    }
}
