namespace CarRentalSystem.Dealers.Data.Models
{
    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class CarAdSpecs
    {
        [Fact]
        public void ChangeAvailabilityShouldMutateIsAvailable()
        {
            // Arrange
            CarAd carAd = A.Dummy<CarAd>();

            // Act
            carAd.ChangeAvailability();

            // Assert
            carAd.IsAvailable.Should().BeFalse();
        }
    }
}
