namespace CarRentalSystem.Application.Common
{
    public class ApplicationSettings
    {
        public string Secret { get; private set; }

        public ApplicationSettings() => this.Secret = default!;
    }
}
