namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Common;
    using Application.Common.Contracts;

    using Commands.Common;

    using Domain.Common;
    using Domain.Models.CarAds;
    using Domain.Repositories;

    using MediatR;

    public class EditCarAdCommand : CarAdCommand<EditCarAdCommand>, IRequest<Result>
    {
        public class EditCarAdCommandHandler : IRequestHandler<EditCarAdCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdDomainRepository carAdRepository;
            private readonly IDealerDomainRepository dealerRepository;

            public EditCarAdCommandHandler(
                ICurrentUser currentUser,
                ICarAdDomainRepository carAdRepository,
                IDealerDomainRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(EditCarAdCommand request, CancellationToken cancellationToken)
            {
                bool hasDealerACarAd = await currentUser.HasDealerACarAd(
                    dealerRepository,
                    request.Id,
                    cancellationToken);

                if (!hasDealerACarAd)
                {
                    return hasDealerACarAd;
                }

                Category category = await carAdRepository.GetCategory(request.Category, cancellationToken);

                CarAd carAd = await carAdRepository.Find(request.Id, cancellationToken);

                carAd
                   .UpdateManufacturer(request.Manufacturer)
                   .UpdateModel(request.Model)
                   .UpdateCategory(category)
                   .UpdateImageUrl(request.ImageUrl)
                   .UpdatePricePerDay(request.PricePerDay)
                   .UpdateOptions(
                       request.HasClimateControl,
                       request.NumberOfSeats,
                       Enumeration.FromValue<TransmissionType>(request.TransmissionType));

                await carAdRepository.Save(carAd, cancellationToken);

                return Result.Success;
            }
        }
    }
}
