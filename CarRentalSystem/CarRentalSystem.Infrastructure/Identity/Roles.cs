namespace CarRentalSystem.Infrastructure.Identity
{
    internal static class Roles
    {
        public static string User => nameof(User);
        public static string UserDescription => "Perform normal operations.";

        public static string Administrator => nameof(Administrator);
        public static string AdministratorDescription => "Perform all the operations.";
    }
}
