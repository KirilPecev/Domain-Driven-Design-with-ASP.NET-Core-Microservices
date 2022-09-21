namespace CarRentalSystem.Infrastructure.Statistics
{
    using Common.Persistence;

    using Domain.Statistics.Models.Statistics;

    using Microsoft.EntityFrameworkCore;

    internal interface IStatisticsDbContext : IDbContext
    {
        DbSet<Statistic> Statistics { get; }

        DbSet<CarAdView> CarAdViews { get; }
    }
}
