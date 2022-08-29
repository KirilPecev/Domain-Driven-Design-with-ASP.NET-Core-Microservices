namespace CarRentalSystem.Application.Features.CarAds.ChangeAvailability
{
    using Application.Common;
    using Commands.Common;
    using Contracts;
    using Dealers;
    using Domain.Models.CarAds;

    using MediatR;

    public class ChangeAvailabilityCommand : EntityCommand<int>, IRequest<Result>
    {
        public class ChangeAvailabilityCommandHandler : IRequestHandler<ChangeAvailabilityCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdRepository carAdRepository;
            private readonly IDealerRepository dealerRepository;

            public ChangeAvailabilityCommandHandler(
                ICurrentUser currentUser,
                ICarAdRepository carAdRepository,
                IDealerRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(ChangeAvailabilityCommand request, CancellationToken cancellationToken)
            {
                bool hasDealerACarAd = await this.currentUser.HasDealerACarAd(
                    this.dealerRepository,
                    request.Id,
                    cancellationToken);

                if (!hasDealerACarAd)
                {
                    return hasDealerACarAd;
                }

                CarAd carAd = await this.carAdRepository.Find(request.Id, cancellationToken);

                carAd.ChangeAvailability();

                await this.carAdRepository.Save(carAd, cancellationToken);

                return Result.Success;
            }
        }
    }
}
