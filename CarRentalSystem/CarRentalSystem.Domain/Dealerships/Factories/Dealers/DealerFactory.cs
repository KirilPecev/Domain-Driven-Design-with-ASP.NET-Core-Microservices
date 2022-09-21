namespace CarRentalSystem.Domain.Dealerships.Factories.Dealers
{
    using Models.Dealers;

    internal class DealerFactory : IDealerFactory
    {
        private string dealerName = default!;
        private string dealerPhoneNumber = default!;

        public Dealer Build() => new Dealer(dealerName, dealerPhoneNumber);

        public IDealerFactory WithName(string name)
        {
            dealerName = name;
            return this;
        }

        public IDealerFactory WithPhoneNumber(string phoneNumber)
        {
            dealerPhoneNumber = phoneNumber;
            return this;
        }
    }
}
