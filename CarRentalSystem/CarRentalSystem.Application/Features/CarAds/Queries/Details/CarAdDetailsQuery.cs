﻿namespace CarRentalSystem.Application.Features.CarAds.Queries.Details
{
    using Dealers;
    using MediatR;

    public class CarAdDetailsQuery : EntityCommand<int>, IRequest<CarAdDetailsOutputModel>
    {
        public class CarAdDetailsQueryHandler : IRequestHandler<CarAdDetailsQuery, CarAdDetailsOutputModel>
        {
            private readonly ICarAdRepository carAdRepository;
            private readonly IDealerRepository dealerRepository;

            public CarAdDetailsQueryHandler(ICarAdRepository carAdRepository, IDealerRepository dealerRepository)
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
