namespace CarRentalSystem.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;

    using static Constants;

    public class AuthorizeAdministratorAndUserAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAndUserAttribute() => this.Roles = $"{AdministratorRoleName},{UserRoleName}";
    }
}
