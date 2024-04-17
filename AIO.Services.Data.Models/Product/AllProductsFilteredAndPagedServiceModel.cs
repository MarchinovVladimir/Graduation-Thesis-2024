using AIO.Web.ViewModels.Product;

namespace AIO.Services.Data.Models.Product
{
	/// <summary>
	/// All products filtered and paged service model.
	/// </summary>
	public class AllProductsFilteredAndPagedServiceModel
	{
		/// <summary>
		/// TotalProductsCount property of the AllProductsFilteredAndPagedServiceModel.
		/// </summary>
		public int TotalProductsCount { get; set; }

		/// <summary>
		/// Products property of the AllProductsFilteredAndPagedServiceModel.
		/// </summary>
		public IEnumerable<ProductAllViewModel> Products { get; set; } =
			new HashSet<ProductAllViewModel>();
	}
}
