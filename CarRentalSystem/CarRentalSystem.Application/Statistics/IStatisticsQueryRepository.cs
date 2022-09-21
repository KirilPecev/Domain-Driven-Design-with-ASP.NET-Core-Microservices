namespace CarRentalSystem.Application.Statistics
{
    using Common.Contracts;

    using Domain.Statistics.Models.Statistics;

    using Queries.Current;

    public interface IStatisticsQueryRepository : IQueryRepository<Statistic>
    {
        Task<int> GetCarAdViews(int carAdId, CancellationToken cancellationToken);

        Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken);
    }
}
