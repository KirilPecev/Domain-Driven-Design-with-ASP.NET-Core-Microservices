namespace CarRentalSystem.Domain.Models.CarAds
{
    using FakeItEasy;

    public class OptionsFakes
    {
        public class OptionsDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Options);

            public object? Create(Type type)
                => new Options(true, 4, TransmissionType.Automatic);
        }
    }
}
