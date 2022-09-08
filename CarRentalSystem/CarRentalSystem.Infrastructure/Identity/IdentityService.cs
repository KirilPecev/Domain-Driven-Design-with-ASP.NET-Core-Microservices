namespace CarRentalSystem.Infrastructure.Identity
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Application;
    using Application.Common;
    using Application.Features.Identity;
    using Application.Features.Identity.Commands;
    using Application.Features.Identity.Commands.ChangePassword;
    using Application.Features.Identity.Commands.LoginUser;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentity
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityService(
            UserManager<User> userManager,
            IOptions<ApplicationSettings> applicationSettings,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.applicationSettings = applicationSettings.Value;
            this.roleManager = roleManager;
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

            string token = this.GenerateJwtToken(user.Id, user.Email);

            return new LoginSuccessModel(user.Id, token);
        }

        public async Task<Result<IUser>> Register(UserInputModel userInput)
        {
            User user = new User(userInput.Email);

            IdentityResult identityResult = await this.userManager.CreateAsync(user, userInput.Password);

            IEnumerable<string> errors = identityResult.Errors.Select(e => e.Description);

            //if (identityResult.Succeeded)
            //{
            //    this.userManager.AddToRoleAsync(user, "");
            //}

            return identityResult.Succeeded ? Result<IUser>.SuccessWith(user) : Result<IUser>.Failure(errors);
        }

        private string GenerateJwtToken(string userId, string email)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, email),
                    //new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(this.applicationSettings.TokenExpirationDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
