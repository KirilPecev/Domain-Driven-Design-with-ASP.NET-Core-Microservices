namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    public class LoginSuccessModel
    {
        public string UserId { get; }

        public string Token { get; }

        public LoginSuccessModel(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }
    }
}
