namespace CarRentalSystem.Infrastructure.Identity.Seeders
{
    using Microsoft.AspNetCore.Identity;

    using static CarRentalSystem.Application.Identity.Common.UserRoles;

    internal static class IdentityUserSeeder
    {
        private static Dictionary<string, string> roles = new()
        {
            { User, UserDescription },
            { Administrator, AdministratorDescription }
        };

        public static void SeedData(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var item in roles)
            {
                bool exists = roleManager.RoleExistsAsync(item.Key).Result;

                if (!exists)
                {
                    Role role = new Role(item.Key, item.Value);
                    _ = roleManager.CreateAsync(role).Result;
                }
            }
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            bool exists = userManager.FindByNameAsync("user1").Result != null;
            if (!exists)
            {
                User user = new User("user1@localhost");

                IdentityResult result = userManager.CreateAsync(user, "user1@localhost123").Result;

                if (result.Succeeded)
                {
                    _ = userManager.AddToRoleAsync(user, Administrator).Result;
                    _ = userManager.AddToRoleAsync(user, User).Result;
                }
            }
        }
    }
}
