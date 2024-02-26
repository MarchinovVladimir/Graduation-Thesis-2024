using AIO.Web.ViewModels.Product;

namespace AIO.Services.Data.Models.Product
{
	public class AllProductsFilteredAndPagedServiceModel
	{
		public int TotalProductsCount { get; set; }

		public IEnumerable<ProductAllViewModel> Products { get; set; } = new HashSet<ProductAllViewModel>();
	}
}
