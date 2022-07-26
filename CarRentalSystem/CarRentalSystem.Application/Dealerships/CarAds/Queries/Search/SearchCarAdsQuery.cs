﻿namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common;

    using MediatR;

    public class SearchCarAdsQuery : CarAdsQuery, IRequest<SearchCarAdsOutputModel>
    {
        public class SearchCarAdsQueryHandler : CarAdsQueryHandler, IRequestHandler<SearchCarAdsQuery, SearchCarAdsOutputModel>
        {
            public SearchCarAdsQueryHandler(ICarAdQueryRepository carAdRepository)
               : base(carAdRepository) { }

            public async Task<SearchCarAdsOutputModel> Handle(
                SearchCarAdsQuery request,
                CancellationToken cancellationToken)
            {
                IEnumerable<CarAdOutputModel> carAdListings = await GetCarAdListings<CarAdOutputModel>(
                    request,
                    cancellationToken: cancellationToken);

                int totalPages = await GetTotalPages(
                    request,
                    cancellationToken: cancellationToken);

                return new SearchCarAdsOutputModel(carAdListings, request.Page, totalPages);
            }
        }
    }
}
