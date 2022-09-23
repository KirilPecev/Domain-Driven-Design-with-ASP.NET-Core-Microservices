namespace CarRentalSystem.Domain.Statistics.Models
{
    using Common;

    public class StatisticData : IInitialData
    {
        public Type EntityType => typeof(Statistic);

        public IEnumerable<object> GetData()
            => new List<Statistic>
            {
                new Statistic()
            };
    }
}
