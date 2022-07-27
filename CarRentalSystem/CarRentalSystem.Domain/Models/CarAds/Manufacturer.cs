namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;

    using static ModelConstants.Manufacturer;

    public class Manufacturer : Entity<int>
    {
        public string Name { get; set; }

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
