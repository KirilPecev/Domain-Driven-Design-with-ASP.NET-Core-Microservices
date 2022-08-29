﻿namespace CarRentalSystem.Application.Features.Identity
{
    using Commands;
    using Commands.ChangePassword;
    using Commands.LoginUser;
    using Common;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(UserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInputModel);
    }
}
