﻿namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Delete
{
    using Application.Common;
    using Application.Common.Contracts;

    using Commands.Common;

    using Domain.Dealerships.Repositories;

    using MediatR;

    public class DeleteCarAdCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteCarAdCommandHandler : IRequestHandler<DeleteCarAdCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdDomainRepository carAdRepository;
            private readonly IDealerDomainRepository dealerRepository;

            public DeleteCarAdCommandHandler(
                ICurrentUser currentUser,
                ICarAdDomainRepository carAdRepository,
                IDealerDomainRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(DeleteCarAdCommand request, CancellationToken cancellationToken)
            {
                bool dealerHasCar = await currentUser.HasDealerACarAd(
                    dealerRepository,
                    request.Id,
                    cancellationToken);

                if (!dealerHasCar)
                {
                    return dealerHasCar;
                }

                return await carAdRepository.Delete(request.Id, cancellationToken);
            }
        }
    }
}
