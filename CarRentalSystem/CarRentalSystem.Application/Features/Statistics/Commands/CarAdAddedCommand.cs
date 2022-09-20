﻿namespace CarRentalSystem.Application.Features.Statistics.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common;
    using Domain.Repositories;

    using MediatR;

    public class CarAdAddedCommand : EntityCommand<int>, IRequest<Result>
    {
        public class CarAdAddedCommandHandler : IRequestHandler<CarAdAddedCommand, Result>
        {
            private readonly IStatisticsDomainRepository statistics;

            public CarAdAddedCommandHandler(IStatisticsDomainRepository statistics)
            => this.statistics = statistics;

            public async Task<Result> Handle(CarAdAddedCommand request, CancellationToken cancellationToken)
                => await this.statistics.IncrementCarAds(cancellationToken);
        }
    }
}
