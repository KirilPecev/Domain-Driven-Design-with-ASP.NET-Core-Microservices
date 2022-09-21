namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Threading;

    using Application.Contracts;
    using Application.Features.Statistics;
    using Application.Features.Statistics.Queries.Current;

    using AutoMapper;

    using CarRentalSystem.Application.Common.Contracts;
    using CarRentalSystem.Application.Statistics;
    using CarRentalSystem.Application.Statistics.Queries.Current;
    using CarRentalSystem.Domain.Statistics.Models.Statistics;
    using CarRentalSystem.Domain.Statistics.Repositories;

    using Domain.Models.Statistics;
    using Domain.Repositories;

    using Microsoft.EntityFrameworkCore;

    internal class StatisticRepository : DataRepository<Statistic>, IStatisticsDomainRepository, IStatisticsQueryRepository
    {
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;
        private readonly string cacheKey = $"{typeof(Statistic)}";

        public StatisticRepository(CarRentalDbContext db, IMapper mapper, ICacheService cacheService)
            : base(db)
        {
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<int> GetCarAdViews(int carAdId, CancellationToken cancellationToken)
            => await this.Data
                .CarAdViews
                .CountAsync(cav => cav.CarAdId == carAdId, cancellationToken);

        public async Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<GetCurrentStatisticsOutputModel>(this.All())
                .SingleOrDefaultAsync(cancellationToken);

        public async Task<bool> IncrementCarAds(CancellationToken cancellationToken)
        {
            Statistic? statistics = await this.Data
                .Statistics
                .SingleOrDefaultAsync(cancellationToken);

            statistics.AddCarAd();

            await this.Save(statistics, cancellationToken);

            return true;
        }
    }
}
