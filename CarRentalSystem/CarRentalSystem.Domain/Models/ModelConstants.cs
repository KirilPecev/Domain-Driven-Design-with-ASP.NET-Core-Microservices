namespace CarRentalSystem.Domain.Models
{
    public class ModelConstants
    {
        public class User
        {
            public const int MinEmailLength = 5;
            public const int MaxEmailLength = 50;
            public const int MaxPasswordLength = 100;
            public const int MinNameLength = 1;
            public const int MaxNameLength = 100;
        }

        public class Common
        {
            public const int MaxUrlLength = 2048;
            public const int Zero = 0;
        }

        public class Dealer
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class PhoneNumber
        {
            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 20;

            public const string PhoneNumberRegularExpression = @"\+[0-9]*";
        }

        public class Category
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;

            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 1000;
        }

        public class Manufacturer
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Options
        {
            public const int MinNumberOfSeats = 1;
            public const int MaxNumberOfSeats = 30;
        }

        public class CarAd
        {
            public const int MinModelLength = 2;
            public const int MaxModelLength = 20;
        }
    }
}
