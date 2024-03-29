namespace AIOCommon
{
	public static class ErrorMessageConstants
	{
        public static class  Product
        {
            public const string BecomeSellerErrorMessage = "You must become a seller to add products.";
            public const string TitleLengthErrorMessage = 
                "{0} must be between {2} and {1} characters long.";
            public const string DescriptionLengthErrorMessage = 
                "{0} must be between {2} and {1} characters long.";
            public const string URLLengthErrorMessage = 
                "{0} must be between {2} and {1} characters long.";
            public const string URLInvalidErrorMessage = "Invalid URL.";
            public const string PriceRangeErrorMessage = "{0} must be between {2} and {1}";
            public const string GeneralErrorMessage = 
                "Unexpected error occured. Please try again later or contact administrator!";
            public const string CategoryNotExistsErrorMessage = "Selected category does not exist";
            public const string UnsuccesfulProductAddErrorMessage = 
                "An error occurred while adding the product. " +
                "Please try again later or contact administrator!";
            public const string LocationNotExistsErrorMessage = "Selected location does not exist";
            public const string ProductDoesNotExistErrorMessage = "Product does not exist!";
            public const string MustBeSellerToReactivateErrorMessage = 
                "You must be a product's seller to be able to reactivate the product!";
            public const string MustBeSellerToEditOrDeleteErrorMessage = 
                "You must be a product's seller to be able to edit or delete the product!";
		}
    }
}
