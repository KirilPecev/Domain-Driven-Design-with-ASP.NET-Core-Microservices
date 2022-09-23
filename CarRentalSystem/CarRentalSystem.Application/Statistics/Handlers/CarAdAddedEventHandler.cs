namespace CarRentalSystem.Application.Statistics.Commands
{
    using System.Threading.Tasks;

    using Common;

    using Domain.Dealerships.Events;
    using Domain.Statistics.Repositories;

    public class CarAdAddedEventHandler : IEventHandler<CarAdAddedEvent>
    {
        private readonly IStatisticsDomainRepository statistics;

        public CarAdAddedEventHandler(IStatisticsDomainRepository statistics)
        => this.statistics = statistics;

        public Task Handle(CarAdAddedEvent domainEvent)
            => this.statistics.IncrementCarAds();
    }
}
