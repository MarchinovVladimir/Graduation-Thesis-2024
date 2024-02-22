namespace AIOCommon
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Product
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0.01";
        }

        public static class Seller
        {
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;
        }

        public static class Vehicle
        {
            public const int MakeMinLength = 2;
            public const int MakeMaxLength = 50;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;

            public const int EnginePowerMinValue = 1;
            public const int EnginePowerMaxValue = 2000;
            public const int EngineCapacityMinValue = 1;
            public const int EngineCapacityMaxValue = 20000;
            public const int OdometerMinValue = 0;
            public const int OdometerMaxValue = 1000000;
        }

        public static class TransmissionType
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }   

        public static class FuelType
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class SafetyFeatures
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class InteriorFeatures
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class VehicleType
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }   
    }
}
