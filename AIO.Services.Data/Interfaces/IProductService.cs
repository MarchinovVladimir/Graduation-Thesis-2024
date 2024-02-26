using AIO.Web.ViewModels.Home;
using AIO.Web.ViewModels.Product;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts();

		Task CreateProductAsync(AddProductFormModel formModel, string agentId);
	}
}
