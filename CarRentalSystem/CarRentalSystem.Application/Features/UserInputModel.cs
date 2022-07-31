namespace CarRentalSystem.Application.Features
{
    public class UserInputModel
    {
        public string Email { get; }

        public string Password { get; }

        public UserInputModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
