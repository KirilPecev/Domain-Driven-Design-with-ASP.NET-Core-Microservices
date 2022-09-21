namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.ChangeAvailability
{
    using Application.Common;
    using Application.Common.Contracts;

    using Common;

    using Domain.Dealerships.Models.CarAds;
    using Domain.Dealerships.Repositories;

    using MediatR;

    public class ChangeAvailabilityCommand : EntityCommand<int>, IRequest<Result>
    {
        public class ChangeAvailabilityCommandHandler : IRequestHandler<ChangeAvailabilityCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdDomainRepository carAdRepository;
            private readonly IDealerDomainRepository dealerRepository;

            public ChangeAvailabilityCommandHandler(
                ICurrentUser currentUser,
                ICarAdDomainRepository carAdRepository,
                IDealerDomainRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(ChangeAvailabilityCommand request, CancellationToken cancellationToken)
            {
                bool hasDealerACarAd = await currentUser.HasDealerACarAd(
                    dealerRepository,
                    request.Id,
                    cancellationToken);

                if (!hasDealerACarAd)
                {
                    return hasDealerACarAd;
                }

                CarAd carAd = await carAdRepository.Find(request.Id, cancellationToken);

                carAd.ChangeAvailability();

                await carAdRepository.Save(carAd, cancellationToken);

                return Result.Success;
            }
        }
    }
}
