namespace CarRentalSystem.Domain.Dealerships.Specifications.Dealer
{
    using System;
    using System.Linq.Expressions;

    using Common;

    using Models.Dealers;

    public class DealerByIdSpecification : Specification<Dealer>
    {
        private readonly int? id;

        public DealerByIdSpecification(int? id)
            => this.id = id;

        protected override bool Include => id != null;

        public override Expression<Func<Dealer, bool>> ToExpression()
            => dealer => dealer.Id == id;
    }
}
