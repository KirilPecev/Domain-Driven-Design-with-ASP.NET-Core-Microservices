namespace CarRentalSystem.Application.Statistics.Queries.Current
{
    using AutoMapper;

    using Common.Mapper;

    using Domain.Statistics.Models;

    public class GetCurrentStatisticsOutputModel : IMapFrom<Statistic>
    {
        public int TotalCarAds { get; private set; }

        public int TotalCarAdViews { get; private set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Statistic, GetCurrentStatisticsOutputModel>()
                .ForMember(cs => cs.TotalCarAds, cfg => cfg
                    .MapFrom(s => s.CarAdViews.Count));
    }
}