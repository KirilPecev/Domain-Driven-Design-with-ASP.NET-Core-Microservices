namespace CarRentalSystem.Domain.Dealerships.Specifications.CarAds
{
    using System.Linq.Expressions;

    using Common;

    using Models.CarAds;

    public class CarAdByPricePerDaySpecification : Specification<CarAd>
    {
        private readonly decimal? minPrice;
        private readonly decimal? maxPrice;

        public CarAdByPricePerDaySpecification(decimal? minPrice, decimal? maxPrice)
        {
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
        }

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => minPrice < carAd.PricePerDay && maxPrice > carAd.PricePerDay;
    }
}
