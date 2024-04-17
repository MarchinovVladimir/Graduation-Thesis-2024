using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Seller;
using static AIOCommon.ErrorMessageConstants.Seller;

namespace AIO.Web.ViewModels.Seller
{
	/// <summary>
	/// View model for becoming a seller.
	/// </summary>
	public class BecomeSellerFormModel
	{
		/// <summary>
		/// Phone number property of the BecomeSellerFormModel.
		/// </summary>
		[Required]
		[Phone]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = PhoneNumberLengthErrorMessage)]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
