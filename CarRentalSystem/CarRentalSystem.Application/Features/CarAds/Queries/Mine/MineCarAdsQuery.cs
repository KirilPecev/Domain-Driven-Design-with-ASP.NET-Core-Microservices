namespace CarRentalSystem.Application.Features.CarAds.Queries.Mine
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common;
    using Contracts;
    using Dealers;

    using MediatR;

    public class MineCarAdsQuery : CarAdsQuery, IRequest<MineCarAdsOutputModel>
    {
        public class MineCarAdsQueryHandler : CarAdsQueryHandler, IRequestHandler<MineCarAdsQuery, MineCarAdsOutputModel>
        {
            private readonly IDealerRepository dealerRepository;
            private readonly ICurrentUser currentUser;

            public MineCarAdsQueryHandler(
                ICarAdRepository carAdRepository,
                IDealerRepository dealerRepository,
                ICurrentUser currentUser) : base(carAdRepository)
            {
                this.dealerRepository = dealerRepository;
                this.currentUser = currentUser;
            }

            public async Task<MineCarAdsOutputModel> Handle(MineCarAdsQuery request, CancellationToken cancellationToken)
            {
                int dealerId = await this.dealerRepository.GetDealerId(
                   this.currentUser.UserId,
                   cancellationToken);

                IEnumerable<MineCarAdOutputModel> mineCarAdListings = await base.GetCarAdListings<MineCarAdOutputModel>(
                    request,
                    onlyAvailable: false,
                    dealerId,
                    cancellationToken);

                int totalPages = await base.GetTotalPages(
                    request,
                    onlyAvailable: false,
                    dealerId,
                    cancellationToken);

                return new MineCarAdsOutputModel(mineCarAdListings, request.Page, totalPages);
            }
        }
    }
}
