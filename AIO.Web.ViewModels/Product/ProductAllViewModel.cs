using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Product
{
	public class ProductAllViewModel
	{
		public string Id { get; set; } = null!;

		public string Title { get; set; } = null!;

		[Display(Name = "Image link")]
		public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
