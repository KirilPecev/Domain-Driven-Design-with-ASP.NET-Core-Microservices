namespace CarRentalSystem.Domain.Dealerships.Exceptions
{
    using Common;

    public class InvalidCarAdException : BaseDomainException
    {
        public InvalidCarAdException() { }

        public InvalidCarAdException(string message) => this.Message = message;
    }
}
