namespace CarRentalSystem.Dealers.Data.Models
{
    using Exceptions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class ManufacturerSpecs
    {
        [Fact]
        public void ValidManufacturerShouldNotThrowException()
        {
            // Act
            Action act = () => A.Dummy<Manufacturer>();

            // Assert
            act.Should().NotThrow<InvalidCarAdException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Manufacturer("");

            // Assert
            act.Should().Throw<InvalidCarAdException>();
        }
    }
}
