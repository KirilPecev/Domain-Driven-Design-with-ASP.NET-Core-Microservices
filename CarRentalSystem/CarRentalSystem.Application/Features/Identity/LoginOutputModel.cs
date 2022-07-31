namespace CarRentalSystem.Application.Features.Identity
{
    public class LoginOutputModel
    {
        public string Token { get; }

        public LoginOutputModel(string token) => Token = token;
    }
}
