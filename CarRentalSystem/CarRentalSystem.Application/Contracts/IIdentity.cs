namespace CarRentalSystem.Application.Contracts
{
    using Features.Identity;

    public interface IIdentity
    {
        Task<Result> Register(UserInputModel userInput);

        Task<Result<LoginOutputModel>> Login(UserInputModel userInput);
    }
}
