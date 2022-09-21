namespace CarRentalSystem.Domain.Statistics.Models.Statistics
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
