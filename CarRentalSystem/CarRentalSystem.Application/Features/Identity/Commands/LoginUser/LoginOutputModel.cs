namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public string Token { get; }

        public int DealerId { get; }

        public LoginOutputModel(string token, int dealerId)
        {
            this.Token = token;
            this.DealerId = dealerId;
        }
    }
}
