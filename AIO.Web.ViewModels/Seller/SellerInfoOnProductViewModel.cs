using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Seller
{
	public class SellerInfoOnProductViewModel
	{
		public string FullName { get; set; } = null!;

		[Required]
		public string Email { get; set; } = null!;

		[Required]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;
	}
}
