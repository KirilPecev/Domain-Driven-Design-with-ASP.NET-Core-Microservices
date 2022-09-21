namespace CarRentalSystem.Infrastructure
{
    using System.Reflection;
    using CarRentalSystem.Domain.Dealerships.Repositories;
    using Domain.Repositories;

    using FluentAssertions;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Persistence;

    using Xunit;

    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void AddRepositoriesShouldRegisterRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddDbContext<CarRentalDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()));

            // Act
            var services = serviceCollection
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
