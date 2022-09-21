namespace CarRentalSystem.Application.Identity.Commands.RegisterUser
{
    using Bogus;

    public class RegisterUserCommandFakes
    {
        public static class Data
        {
            public static RegisterUserCommand GetCommand()
                => new Faker<RegisterUserCommand>()
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.Password, f => f.Lorem.Letter(10))
                    .RuleFor(u => u.Name, f => f.Name.FullName())
                    .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("+#######"))
                    .Generate();
        }
    }
}
