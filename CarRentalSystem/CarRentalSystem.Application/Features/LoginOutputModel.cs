namespace CarRentalSystem.Application.Features
{
    public class LoginOutputModel
    {
        public string Token { get; }

        public LoginOutputModel(string token) => this.Token = token;
    }
}
