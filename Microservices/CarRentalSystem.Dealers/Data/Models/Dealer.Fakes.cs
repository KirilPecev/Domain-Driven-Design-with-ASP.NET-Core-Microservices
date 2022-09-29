namespace CarRentalSystem.Dealers.Data.Models
{
    using Bogus;

    using Common.Models;

    using FakeItEasy;

    public class DealerFakes
    {
        public class DealerDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Dealer);

            public object? Create(Type type) => Data.GetDealer();
        }

        public static class Data
        {
            public static IEnumerable<Dealer> GetDealers(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(i => GetDealer(i))
                    .Concat(Enumerable
                        .Range(count + 1, count * 2)
                        .Select(i => GetDealer(i)))
                    .ToList();

            public static Dealer GetDealer(int id = 1)
                => new Faker<Dealer>()
                    .CustomInstantiator(f => new(
                        $"Dealer{id}",
                        A.Dummy<PhoneNumber>()
                        ))
                    .Generate()
                    .SetId(id);
        }
    }
}
