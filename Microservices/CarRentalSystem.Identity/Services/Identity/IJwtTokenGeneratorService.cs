namespace CarRentalSystem.Identity.Services.Identity
{
    using System.Collections.Generic;

    using Data.Models;

    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(User user, IEnumerable<string> roles = null);
    }
}
