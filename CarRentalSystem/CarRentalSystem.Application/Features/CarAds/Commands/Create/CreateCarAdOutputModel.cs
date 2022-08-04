namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    public class CreateCarAdOutputModel
    {
        public int CarAdId { get; }

        public CreateCarAdOutputModel(int carAdId) => this.CarAdId = carAdId;
    }
}
