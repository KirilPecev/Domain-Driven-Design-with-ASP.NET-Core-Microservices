namespace CarRentalSystem.Domain.Dealerships.Specifications.CarAds
{
    using System.Linq.Expressions;

    using Common;

    using Models.CarAds;

    public class CarAdByCategorySpecification : Specification<CarAd>
    {
        private readonly int? category;

        public CarAdByCategorySpecification(int? category)
            => this.category = category;

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => carAd.Category.Id == category;
    }
}
