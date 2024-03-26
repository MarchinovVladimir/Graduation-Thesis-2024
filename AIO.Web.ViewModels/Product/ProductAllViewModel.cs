using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.Product
{
	/// <summary>
	/// View model for all products.
	/// </summary>
	public class ProductAllViewModel
	{
		/// <summary>
		/// Id property of the ProductAllViewModel.
		/// </summary>
		public string Id { get; set; } = null!;

		/// <summary>
		/// Title property of the ProductAllViewModel.
		/// </summary>
		public string Title { get; set; } = null!;

		/// <summary>
		/// ImageUrl property of the ProductAllViewModel.
		/// </summary>
		[Display(Name = "Image link")]
		public string ImageUrl { get; set; } = null!;

		/// <summary>
		/// Price property of the ProductAllViewModel.
		/// </summary>
        public decimal Price { get; set; }
    }
}
