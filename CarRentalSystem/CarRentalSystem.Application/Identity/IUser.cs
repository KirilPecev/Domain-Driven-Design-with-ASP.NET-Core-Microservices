namespace CarRentalSystem.Application.Identity
{
    using Domain.Models.Dealers;

    public interface IUser
    {
        void BecomeDealer(Dealer dealer);
    }
}
