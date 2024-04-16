namespace AIOCommon
{
    public static class EntityValidationConstants
    {

        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class LocationArea
        {
			public const int NameMinLength = 2;
			public const int NameMaxLength = 50;

            public const int PostCodeMinLength = 2;
            public const int PostCodeMaxLength = 10;
		}

        public static class Product
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMinLength = 0;
            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "1000000000000";
        }

        public static class Seller
        {
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;
        }

        public static class  User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

			public const int FirstNameMinLength = 1;
			public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 50;
        }
    }
}
