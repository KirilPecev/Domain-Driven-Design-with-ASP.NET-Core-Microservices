namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using Common;
    using Common.Models;

    using Exceptions;

    using static ModelConstants.Manufacturer;

    public class Manufacturer : Entity<int>, IAuditable
    {
        public string Name { get; private set; }

        internal Manufacturer(string name)
        {
            this.Validate(name);

            Name = name;
        }

        public Manufacturer UpdateName(string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        private void Validate(string name)
            => Guard.ForStringLength<InvalidCarAdException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(Manufacturer));
    }
}
