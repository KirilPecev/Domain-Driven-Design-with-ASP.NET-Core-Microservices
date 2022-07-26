﻿namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Edit
{
    using CarRentalSystem.Domain.Dealerships.Repositories;

    using Common;

    using FluentValidation;

    public class EditCarAdCommandValidator : AbstractValidator<EditCarAdCommand>
    {
        public EditCarAdCommandValidator(ICarAdDomainRepository carAdRepository)
            => this.Include(new CarAdCommandValidator<EditCarAdCommand>(carAdRepository));
    }
}
