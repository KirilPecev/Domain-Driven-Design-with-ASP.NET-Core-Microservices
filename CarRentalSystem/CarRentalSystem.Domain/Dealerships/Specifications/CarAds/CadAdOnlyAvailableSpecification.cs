namespace CarRentalSystem.Domain.Dealerships.Specifications.CarAds
{
    using System.Linq.Expressions;

    using Common;

    using Models.CarAds;

    public class CadAdOnlyAvailableSpecification : Specification<CarAd>
    {
        private readonly bool onlyAvailable;

        public CadAdOnlyAvailableSpecification(bool onlyAvailable)
            => this.onlyAvailable = onlyAvailable;

        public override Expression<Func<CarAd, bool>> ToExpression()
        {
            if (onlyAvailable)
            {
                return carAd => carAd.IsAvailable;
            }

            return carAd => true;
        }
    }
}
