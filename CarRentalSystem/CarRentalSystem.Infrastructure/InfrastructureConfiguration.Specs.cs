namespace CarRentalSystem.Infrastructure
{
    using System.Reflection;

    using Common.Events;
    using Common.Persistence;

    using Dealerships;

    using Domain.Dealerships.Repositories;

    using FluentAssertions;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Xunit;

    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void AddRepositoriesShouldRegisterRepositories()
        {
            // Arrange
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddDbContext<CarRentalDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IDealershipDbContext>(provider => provider
                    .GetService<CarRentalDbContext>())
                .AddTransient<IEventDispatcher, EventDispatcher>();

            // Act
            ServiceProvider services = serviceCollection
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddRepositories()
                .BuildServiceProvider();

            // Assert
            services
                .GetService<ICarAdDomainRepository>()
                .Should()
                .NotBeNull();
        }
    }
}
