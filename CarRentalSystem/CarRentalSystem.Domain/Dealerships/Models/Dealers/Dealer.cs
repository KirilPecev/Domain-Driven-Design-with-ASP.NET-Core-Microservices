namespace CarRentalSystem.Domain.Dealerships.Models.Dealers
{
    using CarAds;

    using Common;
    using Common.Models;

    using Exceptions;

    using static ModelConstants.Dealer;

    public class Dealer : Entity<int>, IAggregateRoot, IAuditable
    {
        private readonly HashSet<CarAd> carAds;

        public string Name { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public IReadOnlyCollection<CarAd> CarAds => carAds.ToList().AsReadOnly();

        internal Dealer(string name, PhoneNumber phoneNumber)
        {
            Validate(name);

            Name = name;
            PhoneNumber = phoneNumber;

            carAds = new HashSet<CarAd>();
        }

        private Dealer(string name)
        {
            Name = name;
            PhoneNumber = default!;

            carAds = new HashSet<CarAd>();
        }

        public Dealer UpdateName(string name)
        {
            Validate(name);
            Name = name;

            return this;
        }

        public Dealer UpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;

            return this;
        }

        public void AddCarAd(CarAd carAd)
        {
            carAds.Add(carAd);

            // TODO: Add RaiseEvent() when carAd is added
        }

        private void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(name, MinNameLength, MaxNameLength, nameof(Dealer));
    }
}
