namespace CarRentalSystem.Infrastructure.Identity
{
    using Application.Common;
    using Application.Identity;
    using Application.Identity.Commands;
    using Application.Identity.Commands.ChangePassword;
    using Application.Identity.Commands.LoginUser;
    using Application.Identity.Common;

    using Microsoft.AspNetCore.Identity;

    public class IdentityService : IIdentity
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public IdentityService(
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInputModel)
        {
            User? user = await this.userManager.FindByIdAsync(changePasswordInputModel.UserId);

            if (user == null)
            {
                return InvalidErrorMessage;
            }

            IdentityResult identityResult = await this.userManager.ChangePasswordAsync(
                user,
                changePasswordInputModel.CurrentPassword,
                changePasswordInputModel.NewPassword);

            IEnumerable<string> errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded ? Result.Success : Result.Failure(errors);
        }

        public async Task<Result<LoginSuccessModel>> Login(UserInputModel userInput)
        {
            User user = await this.userManager.FindByEmailAsync(userInput.Email);
            if (user == null)
            {
                return InvalidErrorMessage;
            }

            bool isPasswordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
            if (!isPasswordValid)
            {
                return InvalidErrorMessage;
            }

            IList<string> roles = await this.userManager.GetRolesAsync(user);

            string token = this.jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginSuccessModel(user.Id, token);
        }

        public async Task<Result<IUser>> Register(UserInputModel userInput)
        {
            User user = new User(userInput.Email);

            IdentityResult identityResult = await this.userManager.CreateAsync(user, userInput.Password);

            IEnumerable<string> errors = identityResult.Errors.Select(e => e.Description);

            if (identityResult.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return identityResult.Succeeded ? Result<IUser>.SuccessWith(user) : Result<IUser>.Failure(errors);
        }
    }
}
