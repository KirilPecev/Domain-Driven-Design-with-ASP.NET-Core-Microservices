namespace CarRentalSystem.Application.Dealerships.Dealers.Commands.Edit
{
    using FluentValidation;

    using static Domain.Models.ModelConstants.Dealer;
    using static Domain.Models.ModelConstants.PhoneNumber;

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
