namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Create
{
    using Common;

    using Domain.Repositories;

    using FluentValidation;

    public class CreateCarAdCommandValidator : AbstractValidator<CreateCarAdCommand>
    {
        public CreateCarAdCommandValidator(ICarAdDomainRepository carAdRepository)
           => Include(new CarAdCommandValidator<CreateCarAdCommand>(carAdRepository));
    }
}
