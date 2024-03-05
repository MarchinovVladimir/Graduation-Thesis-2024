using AIO.Services.Data.Models.Product;
using AIO.Web.ViewModels.Home;
using AIO.Web.ViewModels.Product;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts();

		Task CreateProductAsync(ProductFormModel formModel, string agentId);

		Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsFilteredAndPagedAsync(AllProductsQueryModel queryModel);

		Task<IEnumerable<ProductAllViewModel>> GetAllProductsByAgentIdAsync(string agentId);

		Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId);

		Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId);

		Task<bool> ExistsByIdAsync(string productId);

		Task<ProductFormModel> GetProductFormByIdAsync(string productId);

		Task<bool> IsAgentOwnerOfProductWithIdAsync(string productId, string agentId);

		Task EditProductByIdAndFormModel(string productId, ProductFormModel formModel);

	}
}
