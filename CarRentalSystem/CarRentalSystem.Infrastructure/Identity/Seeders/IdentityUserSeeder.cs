namespace CarRentalSystem.Infrastructure.Identity.Seeders
{
    using Microsoft.AspNetCore.Identity;

    using static RoleConstants;

    internal static class IdentityUserSeeder
    {
        private static Dictionary<string, string> roles = new()
        {
            {User, UserDescription },
            {Administrator, AdministratorDescription}
        };

        public static void SeedData(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static async void SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var item in roles)
            {
                bool exists = await roleManager.RoleExistsAsync(item.Key);

                if (!exists)
                {
                    Role role = new Role(item.Key, item.Value);
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async void SeedUsers(UserManager<User> userManager)
        {
            bool exists = await userManager.FindByNameAsync("user1") != null;
            if (!exists)
            {
                User user = new User("user1@localhost");

                IdentityResult result = await userManager.CreateAsync(user, "user1@localhost123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Administrator);
                }
            }
        }
    }
}
