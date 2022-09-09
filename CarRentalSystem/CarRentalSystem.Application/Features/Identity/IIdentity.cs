namespace CarRentalSystem.Application.Features.Identity
{
    using Application.Common;

    using Commands;
    using Commands.ChangePassword;
    using Commands.LoginUser;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(UserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInputModel);
    }
}
