namespace CarRentalSystem.Application.Statistics
{
    using CarRentalSystem.Domain.Statistics.Models;

    using Common.Contracts;

    using Queries.Current;

    public interface IStatisticsQueryRepository : IQueryRepository<Statistic>
    {
        Task<int> GetCarAdViews(int carAdId, CancellationToken cancellationToken);

        Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken);
    }
}
