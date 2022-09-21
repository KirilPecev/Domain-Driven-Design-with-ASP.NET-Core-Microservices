namespace CarRentalSystem.Application.Common
{
    public class ApplicationSettings
    {
        public string Secret { get; private set; }

        public int TokenExpirationDays { get; private set; }

        public ApplicationSettings() => Secret = default!;
    }
}
