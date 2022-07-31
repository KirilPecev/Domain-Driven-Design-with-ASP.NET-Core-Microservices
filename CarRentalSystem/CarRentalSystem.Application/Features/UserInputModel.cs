﻿namespace CarRentalSystem.Application.Features
{
    public class UserInputModel
    {
        public string Email { get; }

        public string Password { get; }

        public UserInputModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
