namespace CarRentalSystem.Domain.Dealerships.Factories.CarAds
{
    using Exceptions;

    using Models.CarAds;

    internal class CarAdFactory : ICarAdFactory
    {
        private Manufacturer carAdManufacturer = default!;
        private string carAdModel = default!;
        private Category carAdCategory = default!;
        private string carAdImageUrl = default!;
        private decimal carAdPricePerDay = default!;
        private Options carAdOptions = default!;

        private bool manufacturerSet = false;
        private bool categorySet = false;
        private bool optionsSet = false;

        public CarAd Build()
        {
            if (!manufacturerSet || !categorySet || !optionsSet)
            {
                throw new InvalidCarAdException("Manufacturer, category and options must have a value.");
            }

            return new CarAd(
                carAdManufacturer,
                carAdModel,
                carAdCategory,
                carAdImageUrl,
                carAdPricePerDay,
                carAdOptions,
                true);
        }

        public ICarAdFactory WithCategory(string name, string description)
            => WithCategory(new Category(name, description));

        public ICarAdFactory WithCategory(Category category)
        {
            carAdCategory = category;
            categorySet = true;
            return this;
        }

        public ICarAdFactory WithImageUrl(string imageUrl)
        {
            carAdImageUrl = imageUrl;
            return this;
        }

        public ICarAdFactory WithManufacturer(string name)
            => WithManufacturer(new Manufacturer(name));

        public ICarAdFactory WithManufacturer(Manufacturer manufacturer)
        {
            carAdManufacturer = manufacturer;
            manufacturerSet = true;
            return this;
        }

        public ICarAdFactory WithModel(string model)
        {
            carAdModel = model;
            return this;
        }

        public ICarAdFactory WithOptions(bool hasClimateControl, int numberOfSeats, TransmissionType transmissionType)
            => WithOptions(new Options(hasClimateControl, numberOfSeats, transmissionType));

        public ICarAdFactory WithOptions(Options options)
        {
            carAdOptions = options;
            optionsSet = true;
            return this;
        }

        public ICarAdFactory WithPricePerDay(decimal pricePerDay)
        {
            carAdPricePerDay = pricePerDay;
            return this;
        }
    }
}
