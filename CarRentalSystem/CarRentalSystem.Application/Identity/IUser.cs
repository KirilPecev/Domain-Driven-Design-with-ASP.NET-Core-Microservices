namespace CarRentalSystem.Application.Identity
{
    using CarRentalSystem.Domain.Dealerships.Models.Dealers;

    using Domain.Models.Dealers;

    public interface IUser
    {
        void BecomeDealer(Dealer dealer);
    }
}
