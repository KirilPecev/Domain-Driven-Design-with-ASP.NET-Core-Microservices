namespace CarRentalSystem.Application.Features.Identity
{
    using Commands.LoginUser;

    public interface IIdentity
    {
        Task<Result> Register(UserInputModel userInput);

        Task<Result<LoginOutputModel>> Login(UserInputModel userInput);
    }
}
