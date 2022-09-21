namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Common
{
    using System.Linq.Expressions;

    using Application.Common;

    using Domain.Models.CarAds;

    public class CarAdsSortOrder : SortOrder<CarAd>
    {
        public CarAdsSortOrder(string? sortBy, string? order)
            : base(sortBy, order) { }

        public override Expression<Func<CarAd, object>> ToExpression()
            => SortBy switch
            {
                "price" => carAd => carAd.PricePerDay,
                "manufacturer" => carAd => carAd.Manufacturer.Name,
                _ => carAd => carAd.Id
            };
    }
}
