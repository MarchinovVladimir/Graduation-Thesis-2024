using AIO.Web.ViewModels.Seller;
using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Product
{
	public class ProductDetailsViewModel : ProductAllViewModel
	{
		[Required]
		public string Description { get; set; } = null!;

		[Required]
		public string Category { get; set; } = null!;

		[Required]
		public SellerInfoOnProductViewModel Seller { get; set; } = null!;
	}
}
