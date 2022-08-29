namespace CarRentalSystem.Application.Features.CarAds.Commands.Delete
{
    using Application.Common;
    using Contracts;
    using Features.CarAds.Commands.Common;
    using Features.Dealers;

    using MediatR;

    public class DeleteCarAdCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteCarAdCommandHandler : IRequestHandler<DeleteCarAdCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdRepository carAdRepository;
            private readonly IDealerRepository dealerRepository;

            public DeleteCarAdCommandHandler(
                ICurrentUser currentUser,
                ICarAdRepository carAdRepository,
                IDealerRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(DeleteCarAdCommand request, CancellationToken cancellationToken)
            {
                bool dealerHasCar = await this.currentUser.HasDealerACarAd(
                    this.dealerRepository,
                    request.Id,
                    cancellationToken);

                if (!dealerHasCar)
                {
                    return dealerHasCar;
                }

                return await this.carAdRepository.Delete(request.Id, cancellationToken);
            }
        }
    }
}
