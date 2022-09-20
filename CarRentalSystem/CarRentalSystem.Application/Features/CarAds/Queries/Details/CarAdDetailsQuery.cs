namespace CarRentalSystem.Application.Features.CarAds.Queries.Details
{
    using Dealers;

    using MediatR;

    public class CarAdDetailsQuery : EntityCommand<int>, IRequest<CarAdDetailsOutputModel>
    {
        public class CarAdDetailsQueryHandler : IRequestHandler<CarAdDetailsQuery, CarAdDetailsOutputModel>
        {
            private readonly ICarAdQueryRepository carAdRepository;
            private readonly IDealerQueryRepository dealerRepository;

            public CarAdDetailsQueryHandler(ICarAdQueryRepository carAdRepository, IDealerQueryRepository dealerRepository)
            {
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<CarAdDetailsOutputModel> Handle(CarAdDetailsQuery request, CancellationToken cancellationToken)
            {
                CarAdDetailsOutputModel carAdDetails = await this.carAdRepository.GetDetails(request.Id, cancellationToken);

                carAdDetails.Dealer = await this.dealerRepository.GetDetailsByCarId(request.Id, cancellationToken);

                return carAdDetails;
            }
        }
    }
}
