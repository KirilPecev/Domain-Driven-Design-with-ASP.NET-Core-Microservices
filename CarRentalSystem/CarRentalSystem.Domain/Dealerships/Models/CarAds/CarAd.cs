namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using Common;
    using Common.Models;

    using Dealerships.Exceptions;

    using static ModelConstants.CarAd;
    using static ModelConstants.Common;

    public class CarAd : Entity<int>, IAggregateRoot, IAuditable
    {
        private static readonly IEnumerable<Category> AllowedCategories
            = new CategoryData().GetData().Cast<Category>();

        public Manufacturer Manufacturer { get; private set; }

        public string Model { get; private set; }

        public Category Category { get; private set; }

        public string ImageUrl { get; private set; }

        public decimal PricePerDay { get; private set; }

        public Options Options { get; private set; }

        public bool IsAvailable { get; private set; }

        internal CarAd(
            Manufacturer manufacturer,
            string model,
            Category category,
            string imageUrl,
            decimal pricePerDay,
            Options options,
            bool isAvailable)
        {
            Validate(model, imageUrl, pricePerDay);
            ValidateCategory(category);

            Manufacturer = manufacturer;
            Model = model;
            Category = category;
            ImageUrl = imageUrl;
            PricePerDay = pricePerDay;
            Options = options;
            IsAvailable = isAvailable;
        }

        private CarAd(
            string model,
            string imageUrl,
            decimal pricePerDay,
            bool isAvailable)
        {
            Model = model;
            ImageUrl = imageUrl;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;

            Manufacturer = default!;
            Category = default!;
            Options = default!;
        }

        public CarAd UpdateManufacturer(string manufacturer)
        {
            if (Manufacturer.Name != manufacturer)
            {
                Manufacturer = new Manufacturer(manufacturer);
            }

            return this;
        }

        public CarAd UpdateModel(string model)
        {
            ValidateModel(model);

            Model = model;

            return this;
        }

        public CarAd UpdateCategory(Category category)
        {
            ValidateCategory(category);

            Category = category;

            return this;
        }

        public CarAd UpdateImageUrl(string imageUrl)
        {
            ValidateImageUrl(imageUrl);

            ImageUrl = imageUrl;

            return this;
        }

        public CarAd UpdatePricePerDay(decimal pricePerDay)
        {
            ValidatePricePerDay(pricePerDay);

            PricePerDay = pricePerDay;

            return this;
        }

        public CarAd UpdateOptions(
            bool hasClimateControl,
            int numberOfSeats,
            TransmissionType transmissionType)
        {
            Options = new Options(hasClimateControl, numberOfSeats, transmissionType);

            return this;
        }

        public CarAd ChangeAvailability()
        {
            IsAvailable = !IsAvailable;

            return this;
        }

        private void Validate(string model, string imageUrl, decimal pricePerDay)
        {
            ValidateModel(model);
            ValidateImageUrl(imageUrl);
            ValidatePricePerDay(pricePerDay);
        }

        private void ValidateModel(string model)
            => Guard.ForStringLength<InvalidCarAdException>(
                model,
                MinModelLength,
                MaxModelLength,
                nameof(Model));

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidCarAdException>(
                imageUrl,
                nameof(ImageUrl));

        private void ValidatePricePerDay(decimal pricePerDay)
            => Guard.AgainstOutOfRange<InvalidCarAdException>(
                pricePerDay,
                Zero,
                decimal.MaxValue,
                nameof(PricePerDay));

        private void ValidateCategory(Category category)
        {
            string? categoryName = category?.Name;

            if (AllowedCategories.Any(c => c.Name == categoryName)) return;

            string allowedCategoryNames = string.Join(", ", AllowedCategories.Select(c => $"'{c.Name}'"));

            throw new InvalidCarAdException($"'{categoryName}' is not a valid category. Allowed values are: {allowedCategoryNames}.");
        }
    }
}
