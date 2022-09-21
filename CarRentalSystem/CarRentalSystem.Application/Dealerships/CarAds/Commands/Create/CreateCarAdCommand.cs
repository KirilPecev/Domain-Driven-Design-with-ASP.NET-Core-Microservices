namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Create
{
    using Application.Common.Contracts;

    using CarRentalSystem.Domain.Common.Models;
    using CarRentalSystem.Domain.Dealerships.Factories.CarAds;
    using CarRentalSystem.Domain.Dealerships.Models.Dealers;
    using CarRentalSystem.Domain.Dealerships.Repositories;

    using Commands.Common;

    using Domain.Common;
    using Domain.Factories.CarAds;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Repositories;

    using MediatR;

    public class CreateCarAdCommand : CarAdCommand<CreateCarAdCommand>, IRequest<CreateCarAdOutputModel>
    {
        public class CreateCarAdCommandHandler : IRequestHandler<CreateCarAdCommand, CreateCarAdOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IDealerDomainRepository dealerRepository;
            private readonly ICarAdDomainRepository carAdRepository;
            private readonly ICarAdFactory carAdFactory;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser,
                IDealerDomainRepository dealerRepository,
                ICarAdDomainRepository carAdRepository,
                ICarAdFactory carAdFactory)
            {
                this.currentUser = currentUser;
                this.dealerRepository = dealerRepository;
                this.carAdRepository = carAdRepository;
                this.carAdFactory = carAdFactory;
            }

            public async Task<CreateCarAdOutputModel> Handle(CreateCarAdCommand request, CancellationToken cancellationToken)
            {
                string userId = currentUser.UserId;

                Dealer dealer = await dealerRepository.FindByUser(userId, cancellationToken);

                Category category = await carAdRepository.GetCategory(request.Category, cancellationToken);

                Manufacturer manufacturer = await carAdRepository.GetManufacturer(request.Manufacturer, cancellationToken);

                var factory = manufacturer == null
                    ? carAdFactory.WithManufacturer(request.Manufacturer)
                    : carAdFactory.WithManufacturer(manufacturer);

                var carAd = factory
                    .WithModel(request.Model)
                    .WithCategory(category)
                    .WithImageUrl(request.ImageUrl)
                    .WithPricePerDay(request.PricePerDay)
                    .WithOptions(
                        request.HasClimateControl,
                        request.NumberOfSeats,
                        Enumeration.FromValue<TransmissionType>(request.TransmissionType))
                    .Build();

                dealer.AddCarAd(carAd);

                await carAdRepository.Save(carAd, cancellationToken);

                return new CreateCarAdOutputModel(carAd.Id);
            }
        }
    }
}
