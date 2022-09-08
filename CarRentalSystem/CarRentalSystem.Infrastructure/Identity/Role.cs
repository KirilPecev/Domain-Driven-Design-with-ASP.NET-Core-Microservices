namespace CarRentalSystem.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public string Description { get; private set; }

        public Role(string roleName, string description)
            : base(roleName)
        {
            Description = description;
        }
    }
}
