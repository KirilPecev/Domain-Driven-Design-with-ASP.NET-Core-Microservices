namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Mine
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Common.Contracts;
    using CarRentalSystem.Domain.Dealerships.Repositories;
    using Common;

    using Domain.Repositories;

    using MediatR;

    public class MineCarAdsQuery : CarAdsQuery, IRequest<MineCarAdsOutputModel>
    {
        public class MineCarAdsQueryHandler : CarAdsQueryHandler, IRequestHandler<MineCarAdsQuery, MineCarAdsOutputModel>
        {
            private readonly IDealerDomainRepository dealerRepository;
            private readonly ICurrentUser currentUser;

            public MineCarAdsQueryHandler(
                ICarAdQueryRepository carAdRepository,
                IDealerDomainRepository dealerRepository,
                ICurrentUser currentUser) : base(carAdRepository)
            {
                this.dealerRepository = dealerRepository;
                this.currentUser = currentUser;
            }

            public async Task<MineCarAdsOutputModel> Handle(MineCarAdsQuery request, CancellationToken cancellationToken)
            {
                int dealerId = await dealerRepository.GetDealerId(
                   currentUser.UserId,
                   cancellationToken);

                IEnumerable<MineCarAdOutputModel> mineCarAdListings = await GetCarAdListings<MineCarAdOutputModel>(
                    request,
                    onlyAvailable: false,
                    dealerId,
                    cancellationToken);

                int totalPages = await GetTotalPages(
                    request,
                    onlyAvailable: false,
                    dealerId,
                    cancellationToken);

                return new MineCarAdsOutputModel(mineCarAdListings, request.Page, totalPages);
            }
        }
    }
}
