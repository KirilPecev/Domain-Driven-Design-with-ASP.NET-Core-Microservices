namespace CarRentalSystem.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public string Description { get; private set; }

        internal Role(string name, string description) : base(name)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
