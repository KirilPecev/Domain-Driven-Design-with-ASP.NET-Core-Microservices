namespace CarRentalSystem.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
