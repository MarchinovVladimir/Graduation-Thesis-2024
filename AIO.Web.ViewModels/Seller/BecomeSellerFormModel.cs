using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.Seller;

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
		[MaxLength(PhoneNumberMaxLength)]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
