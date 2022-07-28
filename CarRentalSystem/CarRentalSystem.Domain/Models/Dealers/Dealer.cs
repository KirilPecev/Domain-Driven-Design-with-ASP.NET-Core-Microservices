namespace CarRentalSystem.Domain.Models.Dealers
{
    using CarAds;
    using Common;
    using Exceptions;

    using static ModelConstants.Dealer;

    public class Dealer : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<CarAd> carAds;

        public string Name { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public IReadOnlyCollection<CarAd> CarAds => this.carAds.ToList().AsReadOnly();

        internal Dealer(string name, PhoneNumber phoneNumber)
        {
            this.Validate(name);

            this.Name = name;
            this.PhoneNumber = phoneNumber;

            this.carAds = new HashSet<CarAd>();
        }

        private Dealer(string name)
        {
            this.Name = name;
            this.PhoneNumber = default!;

            this.carAds = new HashSet<CarAd>();
        }

        public Dealer UpdateName(string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        public Dealer UpdatePhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;

            return this;
        }

        public void AddCarAd(CarAd carAd)
        {
            this.carAds.Add(carAd);

            // TODO: Add RaiseEvent() when carAd is added
        }

        private void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(name, MinNameLength, MaxNameLength, nameof(Dealer));
    }
}
