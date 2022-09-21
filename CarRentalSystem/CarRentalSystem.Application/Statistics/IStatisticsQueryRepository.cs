namespace CarRentalSystem.Application.Statistics
{
    using CarRentalSystem.Domain.Statistics.Models.Statistics;

    using Common.Contracts;

    using Domain.Models.Statistics;

    using Queries.Current;

    public interface IStatisticsQueryRepository : IQueryRepository<Statistic>
    {
        Task<int> GetCarAdViews(int carAdId, CancellationToken cancellationToken);

        Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken);
    }
}
