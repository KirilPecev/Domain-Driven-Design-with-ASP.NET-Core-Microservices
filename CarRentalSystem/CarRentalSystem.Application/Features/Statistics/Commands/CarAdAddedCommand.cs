namespace CarRentalSystem.Application.Features.Statistics.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common;
    using MediatR;

    public class CarAdAddedCommand : EntityCommand<int>, IRequest<Result>
    {
        public class CarAdAddedCommandHandler : IRequestHandler<CarAdAddedCommand, Result>
        {
            private readonly IStatisticRepository statistics;

            public CarAdAddedCommandHandler(IStatisticRepository statistics)
            => this.statistics = statistics;

            public async Task<Result> Handle(CarAdAddedCommand request, CancellationToken cancellationToken)
                => await this.statistics.IncrementCarAds(cancellationToken);
        }
    }
}
