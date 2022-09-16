namespace CarRentalSystem.Application.Features.Statistics.Queries.Current
{
    using AutoMapper;
    using Domain.Models.Statistics;
    using Mapper;

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