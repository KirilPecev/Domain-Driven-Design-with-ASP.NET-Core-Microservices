namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;

    using static ModelConstants.Options;

    public class Options : ValueObject
    {
        public bool HasClimateControl { get; }

        public int NumberOfSeats { get; }

        public TransmissionType TransmissionType { get; }

        internal Options(bool hasClimateControl, int numberOfSeats, TransmissionType transmissionType)
        {
            this.Validate(numberOfSeats);

            this.HasClimateControl = hasClimateControl;
            this.NumberOfSeats = numberOfSeats;
            this.TransmissionType = transmissionType;
        }

        private Options(bool hasClimateControl, int numberOfSeats)
        {
            this.HasClimateControl = hasClimateControl;
            this.NumberOfSeats = numberOfSeats;

            this.TransmissionType = default!;
        }

        private void Validate(int numberOfSeats)
            => Guard.AgainstOutOfRange<InvalidOptionsException>(
                numberOfSeats,
                MinNumberOfSeats,
                MaxNumberOfSeats,
                nameof(this.NumberOfSeats));
    }
}
