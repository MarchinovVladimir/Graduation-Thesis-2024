using AIO.Web.ViewModels.Product;

namespace AIO.Areas.Admin.ViewModels
{
	public class MyProductsViewModel
	{
		public IEnumerable<ProductAllViewModel> AddedProducts { get; set; } = 
			new HashSet<ProductAllViewModel>();
	}
}
