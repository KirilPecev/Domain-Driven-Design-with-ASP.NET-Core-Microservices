namespace CarRentalSystem.Domain.Models.CarAds
{
    using System;

    using FakeItEasy;

    public class CarAdFakes
    {
        public class CarAdDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(CarAdDummyFactory);

            public object? Create(Type type)
                => new CarAd(
                    new Manufacturer("Valid manufacturer"),
                    "Valid model",
                    new Category("Valid category", "Valid description text"),
                    "https://valid.test",
                    10,
                    new Options(true, 4, TransmissionType.Automatic),
                    true);
        }
    }
}
