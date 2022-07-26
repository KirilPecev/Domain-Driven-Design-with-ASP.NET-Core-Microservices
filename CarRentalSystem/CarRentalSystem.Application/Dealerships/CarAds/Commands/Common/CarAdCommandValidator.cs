﻿namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Common
{
    using System;

    using Application.Common;

    using Domain.Common.Models;
    using Domain.Dealerships.Models.CarAds;
    using Domain.Dealerships.Repositories;

    using FluentValidation;

    using static Domain.Dealerships.Models.ModelConstants.CarAd;
    using static Domain.Dealerships.Models.ModelConstants.Common;
    using static Domain.Dealerships.Models.ModelConstants.Manufacturer;
    using static Domain.Dealerships.Models.ModelConstants.Options;

    public class CarAdCommandValidator<TCommand> : AbstractValidator<CarAdCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public CarAdCommandValidator(ICarAdDomainRepository carAdRepository)
        {
            RuleFor(c => c.Manufacturer)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            RuleFor(c => c.Model)
                .MinimumLength(MinModelLength)
                .MaximumLength(MaxModelLength)
                .NotEmpty();

            RuleFor(c => c.Category)
                .MustAsync(async (category, token) => await carAdRepository
                    .GetCategory(category, token) != null)
                .WithMessage("'{PropertyName}' does not exist.");

            RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("'{PropertyName}' must be a valid url.")
                .NotEmpty();

            RuleFor(c => c.PricePerDay)
                .InclusiveBetween(Zero, decimal.MaxValue);

            RuleFor(c => c.NumberOfSeats)
                .InclusiveBetween(MinNumberOfSeats, MaxNumberOfSeats);

            RuleFor(c => c.TransmissionType)
                .Must(Enumeration.HasValue<TransmissionType>)
                .WithMessage("'Transmission Type' is not valid.");
        }
    }
}
