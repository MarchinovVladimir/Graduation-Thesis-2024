namespace AIO.Web.ViewModels.Home
{
	/// <summary>
	/// View model for the product which is visiualise at the start page.
	/// </summary>
	public class ProductIndexViewModel
	{
		/// <summary>
		/// Gets or sets the id of the ProductIndexViewModel.
		/// </summary>
		public string Id { get; set; } = null!;

		/// <summary>
		/// Gets or sets the title of the ProductIndexViewModel.
		/// </summary>
		public string Title { get; set; } = null!;

		/// <summary>
		/// Gets or sets the price of the ProductIndexViewModel.
		/// </summary>
		public string ImageUrl { get; set; } = null!;	
	}
}
