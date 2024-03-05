using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Product
{
	public class ProductPreDeleteDetailsViewModel
	{
		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;
	}
}
