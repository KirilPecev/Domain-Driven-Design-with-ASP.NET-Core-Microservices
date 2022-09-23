namespace CarRentalSystem.Infrastructure.Common.Persistence
{
    using System.Reflection;

    using Application.Common;
    using Application.Statistics.Commands;

    using Dealerships;

    using Domain.Dealerships.Events;
    using Domain.Dealerships.Models.Dealers;
    using Domain.Statistics.Models;

    using Events;

    using FluentAssertions;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Statistics;

    using Xunit;

    public class CarRentalDbContextSpecs
    {
        [Fact]
        public async Task RaisedEventsShouldBeHandled()
        {
            // Arrange
            ServiceProvider services = new ServiceCollection()
                .AddDbContext<CarRentalDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IDealershipDbContext>(provider => provider
                    .GetService<CarRentalDbContext>())
                .AddScoped<IStatisticsDbContext>(provider => provider
                    .GetService<CarRentalDbContext>())
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient<IEventHandler<CarAdAddedEvent>, CarAdAddedEventHandler>()
                .AddRepositories()
                .BuildServiceProvider();

            Dealer dealer = DealerFakes.Data.GetDealer();
            CarRentalDbContext? dbContext = services.GetService<CarRentalDbContext>();

            object statisticsToAdd = new StatisticData()
                .GetData()
                .First();

            dbContext.Add(statisticsToAdd);
            await dbContext.SaveChangesAsync();

            // Act
            dbContext.Dealers.Add(dealer);
            await dbContext.SaveChangesAsync();

            // Assert
            var statistics = await dbContext.Statistics.SingleAsync();

            statistics.TotalCarAds.Should().Be(10);
        }
    }
}
