namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public string Token { get; }

        public LoginOutputModel(string token) => Token = token;
    }
}
