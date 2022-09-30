namespace CarRentalSystem.Dealers.Data
{
    public class DataConstants
    {
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
