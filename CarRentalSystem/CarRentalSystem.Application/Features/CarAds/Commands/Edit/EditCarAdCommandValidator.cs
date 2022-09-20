namespace CarRentalSystem.Application.Features.CarAds.Commands.Edit
{
    using CarRentalSystem.Domain.Repositories;
    using Common;

    using FluentValidation;

    public class EditCarAdCommandValidator : AbstractValidator<EditCarAdCommand>
    {
        public EditCarAdCommandValidator(ICarAdDomainRepository carAdRepository)
            => this.Include(new CarAdCommandValidator<EditCarAdCommand>(carAdRepository));
    }
}
