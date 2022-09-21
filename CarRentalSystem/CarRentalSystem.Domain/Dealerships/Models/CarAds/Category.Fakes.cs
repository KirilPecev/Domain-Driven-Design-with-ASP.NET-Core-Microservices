namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using FakeItEasy;

    public class CategoryFakes
    {
        public class CategoryDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Category);

            public object? Create(Type type)
                => new Category("Economy", "Valid description text");
        }
    }
}
