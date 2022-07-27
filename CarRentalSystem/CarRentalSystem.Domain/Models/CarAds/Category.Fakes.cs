using FakeItEasy;

namespace CarRentalSystem.Domain.Models.CarAds
{
    public class CategoryFakes
    {
        public const string ValidCategoryName = "Economy";

        public class CategoryDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Category);

            public object? Create(Type type)
                => new Category(ValidCategoryName, "Valid description text");
        }
    }
}
