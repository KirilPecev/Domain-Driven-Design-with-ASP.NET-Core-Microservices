namespace CarRentalSystem.Application.Features.Statistics
{
    using Common;
    using Contracts;
    using Domain.Models.Statistics;
    using Queries.Current;

    public interface IStatisticRepository : IRepository<Statistic>
    {
        Task<int> GetCarAdViews(int carAdId, CancellationToken cancellationToken);

        Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken);

        Task<Result> IncrementCarAds(CancellationToken cancellationToken);
    }
}
