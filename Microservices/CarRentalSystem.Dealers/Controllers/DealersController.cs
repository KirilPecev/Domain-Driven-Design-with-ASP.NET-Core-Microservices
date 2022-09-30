namespace CarRentalSystem.Dealers.Controllers
{
    using CarRentalSystem.Controllers;
    using CarRentalSystem.Dealers.Data.Models;
    using CarRentalSystem.Dealers.Models.Dealers;
    using CarRentalSystem.Dealers.Services.Dealers;
    using CarRentalSystem.Infrastructure;
    using CarRentalSystem.Services;
    using CarRentalSystem.Services.Identity;

    using Microsoft.AspNetCore.Mvc;

    public class DealersController : ApiController
    {
        private readonly IDealerService dealers;
        private readonly ICurrentUserService currentUser;

        public DealersController(
            IDealerService dealers,
            ICurrentUserService currentUser)
        {
            this.dealers = dealers;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<DealerDetailsOutputModel>> Details(int id) => await this.dealers.GetDetails(id);

        [HttpGet]
        [Route(Id)]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult<int>> GetDealerId()
        {
            string userId = this.currentUser.UserId;

            bool userIsDealer = await this.dealers.IsDealer(userId);

            if (!userIsDealer)
            {
                return this.BadRequest("This user is not a dealer.");
            }

            return await this.dealers.GetIdByUser(this.currentUser.UserId);
        }

        [HttpPost]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult<int>> Create(CreateDealerInputModel input)
        {
            Dealer dealer = new Dealer
            {
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                UserId = this.currentUser.UserId
            };

            this.dealers.Add(dealer);

            await this.dealers.Save();

            return dealer.Id;
        }

        [HttpPut]
        [Route(Id)]
        [AuthorizeAdministratorAndUser]
        public async Task<ActionResult> Edit(int id, EditDealerInputModel input)
        {
            Dealer dealer = this.currentUser.IsAdministrator
                ? await this.dealers.FindById(id)
                : await this.dealers.FindByUser(this.currentUser.UserId);

            if (id != dealer.Id)
            {
                return BadRequest(Result.Failure("You cannot edit this dealer."));
            }

            dealer.Name = input.Name;
            dealer.PhoneNumber = input.PhoneNumber;

            await this.dealers.Save();

            return Ok();
        }

        [HttpGet]
        [AuthorizeAdministrator]
        public async Task<IEnumerable<DealerDetailsOutputModel>> All() => await this.dealers.GetAll();
    }
}
