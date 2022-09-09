namespace CarRentalSystem.Application.Features.Identity.Common
{
    public static class UserRoles
    {
        public const string User = nameof(User);
        public static string UserDescription => "Perform normal operations.";

        public const string Administrator = nameof(Administrator);
        public static string AdministratorDescription => "Perform all the operations.";
    }
}
