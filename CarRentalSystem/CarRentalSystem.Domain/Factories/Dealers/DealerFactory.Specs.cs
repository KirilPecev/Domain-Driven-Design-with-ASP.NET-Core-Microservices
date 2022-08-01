namespace CarRentalSystem.Domain.Factories.Dealers
{
    using FluentAssertions;
    using Xunit;

    public class DealerFactorySpecs
    {
        [Fact]
        public void BuildShouldCreateDealer()
        {
            // Assert
            var dealerFactory = new DealerFactory();

            // Act
            var dealer = dealerFactory
                .WithName("TestDealer")
                .WithPhoneNumber("+3591234567")
                .Build();

            // Assert
            dealer.Should().NotBeNull();
        }
    }
}
