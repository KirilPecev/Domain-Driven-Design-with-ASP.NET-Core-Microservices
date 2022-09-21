namespace CarRentalSystem.Domain.Dealerships.Models.Dealers
{
    using Exceptions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class PhoneNumberSpecs
    {
        [Fact]
        public void ShouldNotThrowErrorOnValidNumber()
        {
            // Act
            Action act = () => A.Dummy<PhoneNumber>();

            //Assert
            act.Should().NotThrow<InvalidPhoneNumberException>();
        }

        [Fact]
        public void ShouldThrowErrorOnInvalidNumber()
        {
            // Act
            Action act = () => new PhoneNumber("1234");

            //Assert
            act.Should().Throw<InvalidPhoneNumberException>();
        }
    }
}
