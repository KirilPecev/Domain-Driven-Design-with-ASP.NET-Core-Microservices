namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using Common;
    using Common.Models;

    using Exceptions;

    using static ModelConstants.Category;

    public class Category : Entity<int>, IAuditable
    {
        public string Name { get; }

        public string Description { get; }

        internal Category(string name, string description)
        {
            this.Validate(name, description);

            Name = name;
            Description = description;
        }

        private void Validate(string name, string description)
        {
            Guard.ForStringLength<InvalidCarAdException>(
               name,
               MinNameLength,
               MaxNameLength,
               nameof(this.Name));

            Guard.ForStringLength<InvalidCarAdException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }
    }
}
