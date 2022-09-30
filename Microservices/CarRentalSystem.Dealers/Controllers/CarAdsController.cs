namespace CarRentalSystem.Dealers.Controllers
{
    using CarRentalSystem.Controllers;
    using CarRentalSystem.Dealers.Models.Categories;
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Services;
    using CarRentalSystem.Services.Identity;

    using Dealers.Data.Models;

    using Messages.Dealers;

    using Microsoft.AspNetCore.Mvc;

    using Models.CarAds;

    using Services.CarAds;
    using Services.Categories;
    using Services.Dealers;
    using Services.Manufacturers;

    public class CarAdsController : ApiController
    {
        private readonly ICarAdService carAds;
        private readonly IDealerService dealers;
        private readonly ICategoryService categories;
        private readonly IManufacturerService manufacturers;
        private readonly ICurrentUserService currentUser;

        public CarAdsController(
            ICarAdService carAds,
            IDealerService dealers,
            ICategoryService categories,
            IManufacturerService manufacturers,
            ICurrentUserService currentUser)
        {
            this.carAds = carAds;
            this.dealers = dealers;
            this.categories = categories;
            this.manufacturers = manufacturers;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search([FromQuery] CarAdsQuery query)
        {
            IEnumerable<CarAdOutputModel> carAdListings = await this.carAds.GetListings(query);

            int totalCarAds = await this.carAds.Total(query);

            return new SearchCarAdsOutputModel(carAdListings, query.Page, totalCarAds);
        }

        [HttpPost]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(CarAdInputModel input)
        {
            Dealer dealer = await this.dealers.FindByUser(this.currentUser.UserId);

            Category category = await this.categories.Find(input.Category);

            if (category == null)
            {
                return BadRequest(Result.Failure("Category does not exist."));
            }

            Manufacturer manufacturer = await this.manufacturers.FindByName(input.Manufacturer);

            manufacturer ??= new Manufacturer
            {
                Name = input.Manufacturer
            };

            CarAd carAd = new CarAd
            {
                Dealer = dealer,
                Manufacturer = manufacturer,
                Model = input.Model,
                Category = category,
                ImageUrl = input.ImageUrl,
                PricePerDay = input.PricePerDay,
                Options = new Options
                {
                    HasClimateControl = input.HasClimateControl,
                    NumberOfSeats = input.NumberOfSeats,
                    TransmissionType = input.TransmissionType
                }
            };

            this.carAds.Add(carAd);

            CarAdCreatedMessage message = new CarAdCreatedMessage
            {
                CarAdId = carAd.Id,
                Manufacturer = carAd.Manufacturer.Name,
                Model = carAd.Model,
                PricePerDay = carAd.PricePerDay
            };

            await this.carAds.Save(message);

            return new CreateCarAdOutputModel(carAd.Id);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CarAdDetailsOutputModel>> Details(int id) => await this.carAds.GetDetails(id);

        [HttpGet]
        [Route(nameof(Mine))]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine([FromQuery] CarAdsQuery query)
        {
            int dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            IEnumerable<MineCarAdOutputModel> carAdListings = await this.carAds.Mine(dealerId, query);

            int totalCarAds = await this.carAds.Total(query);

            return new MineCarAdsOutputModel(carAdListings, query.Page, totalCarAds);
        }

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<IEnumerable<CategoryOutputModel>> Categories() => await this.categories.GetAll();

        [HttpPut]
        [Route(Id)]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult> Edit(int id, CarAdInputModel input)
        {
            int dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            bool dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            Category category = await this.categories.Find(input.Category);

            Manufacturer manufacturer = await this.manufacturers.FindByName(input.Manufacturer);

            manufacturer ??= new Manufacturer
            {
                Name = input.Manufacturer
            };

            CarAd carAd = await this.carAds.Find(id);

            carAd.Manufacturer = manufacturer;
            carAd.Model = input.Model;
            carAd.Category = category;
            carAd.ImageUrl = input.ImageUrl;
            carAd.PricePerDay = input.PricePerDay;
            carAd.Options = new Options
            {
                HasClimateControl = input.HasClimateControl,
                NumberOfSeats = input.NumberOfSeats,
                TransmissionType = input.TransmissionType
            };

            CarAdUpdatedMessage message = new CarAdUpdatedMessage
            {
                CarAdId = carAd.Id,
                Manufacturer = carAd.Manufacturer.Name,
                Model = carAd.Model
            };

            await this.carAds.Save(message);

            return Result.Success;
        }

        [HttpPut]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult> ChangeAvailability(int id)
        {
            int dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            bool dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            CarAd carAd = await this.carAds.Find(id);

            carAd.IsAvailable = !carAd.IsAvailable;

            await this.carAds.Save();

            return Result.Success;
        }

        [HttpDelete]
        [Route(Id)]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            int dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            bool dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            return await this.carAds.Delete(id);
        }
    }
}
