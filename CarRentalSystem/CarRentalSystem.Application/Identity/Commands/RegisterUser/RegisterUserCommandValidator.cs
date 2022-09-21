namespace CarRentalSystem.Application.Identity.Commands.RegisterUser
{
    using FluentValidation;

    using static Domain.Models.ModelConstants.PhoneNumber;
    using static Domain.Models.ModelConstants.User;

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .MinimumLength(MinEmailLength)
                .MaximumLength(MaxEmailLength)
                .EmailAddress()
                .NotEmpty();

            RuleFor(u => u.Password)
                .MaximumLength(MaxPasswordLength)
                .NotEmpty();

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
