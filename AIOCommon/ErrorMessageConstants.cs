using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOCommon
{
	public static class ErrorMessageConstants
	{
        public static class  Product
        {
            public const string BecomeSellerErrorMessage = "You must become a seller to add products.";
            public const string TitleLengthErrorMessage = "{0} must be between {2} and {1} characters long.";
            public const string DescriptionLengthErrorMessage = "{0} must be between {2} and {1} characters long.";
            public const string URLLengthErrorMessage = "{0} must be between {2} and {1} characters long.";
            public const string URLInvalidErrorMessage = "Invalid URL.";
            public const string PriceRangeErrorMessage = "{0} must be between {2} and {1}";
            public const string GeneralErrorMessage = "Unexpected error occured. Please try again later or contact administrator!";
		}
    }
}
