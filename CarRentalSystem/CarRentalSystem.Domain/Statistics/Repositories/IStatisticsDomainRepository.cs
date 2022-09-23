namespace CarRentalSystem.Domain.Statistics.Repositories
{
    using Common;

    using Models;

    public interface IStatisticsDomainRepository : IDomainRepository<Statistic>
    {
        Task<bool> IncrementCarAds(CancellationToken cancellationToken = default);
    }
}
