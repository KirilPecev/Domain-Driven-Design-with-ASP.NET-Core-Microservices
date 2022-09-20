namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    using Common;
    using Domain.Repositories;
    using FluentValidation;

    public class CreateCarAdCommandValidator : AbstractValidator<CreateCarAdCommand>
    {
        public CreateCarAdCommandValidator(ICarAdDomainRepository carAdRepository)
           => this.Include(new CarAdCommandValidator<CreateCarAdCommand>(carAdRepository));
    }
}
