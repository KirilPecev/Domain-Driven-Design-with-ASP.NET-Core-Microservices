namespace CarRentalSystem.Domain.Models.Dealers
{
    using FakeItEasy;

    public class DealerFakes
    {
        public class DealerDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Dealer);

            public object? Create(Type type) => new Dealer("Valid dealer", A.Dummy<PhoneNumber>());
        }
    }
}
