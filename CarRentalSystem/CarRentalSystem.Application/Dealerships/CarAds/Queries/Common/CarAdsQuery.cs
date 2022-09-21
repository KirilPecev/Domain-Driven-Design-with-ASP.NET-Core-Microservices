namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Common
{
    using CarRentalSystem.Domain.Common;
    using CarRentalSystem.Domain.Dealerships.Models.CarAds;
    using CarRentalSystem.Domain.Dealerships.Models.Dealers;

    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Specifications;
    using Domain.Specifications.CarAds;
    using Domain.Specifications.Dealer;

    public abstract class CarAdsQuery
    {
        private const int CarAdsPerPage = 10;

        public string? Manufacturer { get; set; }

        public int? Category { get; set; }

        public string? Dealer { get; set; }

        public decimal? MinPricePerDay { get; set; }

        public decimal? MaxPricePerDay { get; set; }

        public string? SortBy { get; set; }

        public string? Order { get; set; }

        public int Page { get; set; } = 1;

        public abstract class CarAdsQueryHandler
        {
            private readonly ICarAdQueryRepository carAdRepository;

            protected CarAdsQueryHandler(ICarAdQueryRepository carAdRepository)
                => this.carAdRepository = carAdRepository;

            protected async Task<IEnumerable<TOutputModel>> GetCarAdListings<TOutputModel>(
                CarAdsQuery request,
                bool onlyAvailable = true,
                int? dealerId = default,
                CancellationToken cancellationToken = default)
            {
                Specification<CarAd> carAdSpecification = GetCarAdSpecification(request, onlyAvailable);

                Specification<Dealer> dealerSpecification = GetDealerSpecification(request, dealerId);

                CarAdsSortOrder searchOrder = new CarAdsSortOrder(request.SortBy, request.Order);

                int skip = (request.Page - 1) * CarAdsPerPage;

                return await carAdRepository.GetCarAdListings<TOutputModel>(
                    carAdSpecification,
                    dealerSpecification,
                    searchOrder,
                    skip,
                    take: CarAdsPerPage,
                    cancellationToken);
            }

            protected async Task<int> GetTotalPages(
                CarAdsQuery request,
                bool onlyAvailable = true,
                int? dealerId = default,
                CancellationToken cancellationToken = default)
            {
                Specification<CarAd> carAdSpecification = GetCarAdSpecification(request, onlyAvailable);

                Specification<Dealer> dealerSpecification = GetDealerSpecification(request, dealerId);

                int totalCarAds = await carAdRepository.Total(
                    carAdSpecification,
                    dealerSpecification,
                    cancellationToken);

                return (int)Math.Ceiling((double)totalCarAds / CarAdsPerPage);
            }

            private Specification<CarAd> GetCarAdSpecification(CarAdsQuery request, bool onlyAvailable)
                => new CarAdByManufacturerSpecification(request.Manufacturer)
                    .And(new CarAdByCategorySpecification(request.Category))
                    .And(new CarAdByPricePerDaySpecification(request.MinPricePerDay, request.MaxPricePerDay))
                    .And(new CadAdOnlyAvailableSpecification(onlyAvailable));

            private Specification<Dealer> GetDealerSpecification(CarAdsQuery request, int? dealerId)
                => new DealerByIdSpecification(dealerId)
                    .And(new DealerByNameSpecification(request.Dealer));
        }
    }
}
