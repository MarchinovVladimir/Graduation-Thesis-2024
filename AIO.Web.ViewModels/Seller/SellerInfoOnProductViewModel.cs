using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Seller
{
	/// <summary>
	/// View model for seller information on product.
	/// </summary>
	public class SellerInfoOnProductViewModel
	{
		/// <summary>
		/// Gets or sets the seller's full name.
		/// </summary>
		[Required]
		public string FullName { get; set; } = null!;

		/// <summary>
		/// Gets or sets the seller's email.
		/// </summary>
		[Required]
		public string Email { get; set; } = null!;

		/// <summary>
		/// Gets or sets the seller's phone number.
		/// </summary>
		[Required]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
