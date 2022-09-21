namespace CarRentalSystem.Application.Statistics.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common;

    using Domain.Statistics.Repositories;

    using MediatR;

    public class CarAdAddedCommand : EntityCommand<int>, IRequest<Result>
    {
        public class CarAdAddedCommandHandler : IRequestHandler<CarAdAddedCommand, Result>
        {
            private readonly IStatisticsDomainRepository statistics;

            public CarAdAddedCommandHandler(IStatisticsDomainRepository statistics)
            => this.statistics = statistics;

            public async Task<Result> Handle(CarAdAddedCommand request, CancellationToken cancellationToken)
                => await statistics.IncrementCarAds(cancellationToken);
        }
    }
}
