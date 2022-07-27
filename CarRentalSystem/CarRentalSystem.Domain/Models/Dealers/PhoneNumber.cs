namespace CarRentalSystem.Domain.Models.Dealers
{
    using Common;
    using Exceptions;
    using System.Text.RegularExpressions;

    using static ModelConstants.PhoneNumber;

    public class PhoneNumber : ValueObject
    {
        public string Number { get; }

        internal PhoneNumber(string number)
        {
            this.Validate(number);

            if (!Regex.IsMatch(number, PhoneNumberRegularExpression))
            {
                throw new InvalidPhoneNumberException("Phone number must start with a '+' and contains only digits afterwards.");
            }

            this.Number = number;
        }

        public static implicit operator string(PhoneNumber number) => number.Number;

        public static implicit operator PhoneNumber(string number) => new PhoneNumber(number);

        private void Validate(string number)
            => Guard.ForStringLength<InvalidPhoneNumberException>(number, MinPhoneNumberLength, MaxPhoneNumberLength, nameof(PhoneNumber));
    }
}
