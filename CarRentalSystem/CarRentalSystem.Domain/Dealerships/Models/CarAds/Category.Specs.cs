﻿namespace CarRentalSystem.Domain.Dealerships.Models.CarAds
{
    using Exceptions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class CategorySpecs
    {
        [Fact]
        public void ValidCategoryShouldNotThrowException()
        {
            // Act
            Action act = () => A.Dummy<Category>();

            // Assert
            act.Should().NotThrow<InvalidCarAdException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Category("", "Valid description text");

            // Assert
            act.Should().Throw<InvalidCarAdException>();
        }
    }
}
