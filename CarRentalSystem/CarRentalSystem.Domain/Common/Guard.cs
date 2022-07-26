namespace CarRentalSystem.Domain.Common
{
    using Exceptions;

    public static class Guard
    {
        private const string DefaultName = "Value";

        public static void AgainstEmptyString<TException>(string value, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be null or empty.");
        }

        public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (value.Length >= minLength && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {minLength} and {maxLength} symbols.");
        }

        public static void AgainstOutOfRange<TException>(int number, int min, int max, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            if (number >= min && number <= max)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void AgainstOutOfRange<TException>(decimal number, decimal min, decimal max, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            if (number >= min && number <= max)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void ForValidUrl<TException>(string url, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            if (url.Length <= 100 && //ModelConstants.Common.MaxUrlLength && TODO: remove 100 and use ModelConstants
                Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return;
            }

            ThrowException<TException>($"{name} must be a valid URL.");
        }

        public static void Against<TException>(object actualValue, object unexpectedValue, string name = DefaultName)
            where TException : BaseDomainException, new()
        {
            if (!actualValue.Equals(unexpectedValue))
            {
                return;
            }

            ThrowException<TException>($"{name} must not be {unexpectedValue}.");
        }

        private static void ThrowException<TException>(string message) where TException : BaseDomainException, new()
            => throw new TException { Message = message };
    }
}
