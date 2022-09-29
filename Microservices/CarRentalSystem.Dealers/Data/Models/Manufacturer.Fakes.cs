﻿namespace CarRentalSystem.Dealers.Data.Models
{
    using FakeItEasy;

    public class ManufacturerFakes
    {
        public class ManufacturerDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Manufacturer);

            public object? Create(Type type) => new Manufacturer("Valid manufacturer");
        }
    }
}
