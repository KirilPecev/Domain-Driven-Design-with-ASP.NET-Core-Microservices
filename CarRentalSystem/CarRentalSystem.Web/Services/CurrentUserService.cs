namespace CarRentalSystem.Web.Services
{
    using System.Security.Claims;

    using Application.Contracts;

    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUser
    {
        public string UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException(
                    "This request does not have an authenticated user.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
