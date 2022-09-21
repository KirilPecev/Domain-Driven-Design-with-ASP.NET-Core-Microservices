namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using Exceptions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class OptionsSpecs
    {
        [Fact]
        public void ValidOptionsShouldNotThrowException()
        {
            // Act
            Action act = () => A.Dummy<Options>();

            // Assert
            act.Should().NotThrow<InvalidOptionsException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Options(true, 150, TransmissionType.Automatic);

            // Assert
            act.Should().Throw<InvalidOptionsException>();
        }
    }
}
