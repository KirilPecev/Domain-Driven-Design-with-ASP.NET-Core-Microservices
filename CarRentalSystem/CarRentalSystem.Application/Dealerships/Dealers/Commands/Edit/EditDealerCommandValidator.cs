namespace CarRentalSystem.Application.Dealerships.Dealers.Commands.Edit
{
    using FluentValidation;

    using static CarRentalSystem.Domain.Dealerships.Models.ModelConstants.Dealer;
    using static CarRentalSystem.Domain.Dealerships.Models.ModelConstants.PhoneNumber;

    public class EditDealerCommandValidator : AbstractValidator<EditDealerCommand>
    {
        public EditDealerCommandValidator()
        {
            RuleFor(u => u.Name)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            RuleFor(u => u.PhoneNumber)
                .MinimumLength(MinPhoneNumberLength)
                .MaximumLength(MaxPhoneNumberLength)
                .Matches(PhoneNumberRegularExpression)
                .NotEmpty();
        }
    }
}
