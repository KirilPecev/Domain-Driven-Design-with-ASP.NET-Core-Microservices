namespace CarRentalSystem.Domain.Dealerships.Models.Dealers
{
    using System;

    using FakeItEasy;

    public class PhoneNumberFakes
    {
        public class PhoneNumberDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(PhoneNumber);

            public object? Create(Type type) => new PhoneNumber("+12345678");
        }
    }
}
