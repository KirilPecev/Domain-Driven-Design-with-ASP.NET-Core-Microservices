namespace CarRentalSystem.Domain.Models.CarAds
{
    using System;

    using FakeItEasy;

    public class CarAdFakes
    {
        public class CarAdDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(CarAd);

            public object? Create(Type type)
                => new CarAd(
                    A.Dummy<Manufacturer>(),
                    "Valid model",
                    A.Dummy<Category>(),
                    "https://valid.test",
                    10,
                    A.Dummy<Options>(),
                    true);
        }
    }
}
