namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Mine
{
    using AutoMapper;

    using CarRentalSystem.Domain.Dealerships.Models.CarAds;

    using Common;

    using Domain.Models.CarAds;

    public class MineCarAdOutputModel : CarAdOutputModel
    {
        public bool IsAvailable { get; private set; }

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<CarAd, MineCarAdOutputModel>()
                .IncludeBase<CarAd, CarAdOutputModel>();
    }
}
