namespace CarRentalSystem.Domain.Dealerships.Exceptions
{
    using Common;

    public class InvalidOptionsException : BaseDomainException
    {
        public InvalidOptionsException() { }

        public InvalidOptionsException(string message) => Message = message;
    }
}
