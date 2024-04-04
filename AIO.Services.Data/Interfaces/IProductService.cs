using AIO.Services.Data.Models.Product;
using AIO.Web.ViewModels.Home;
using AIO.Web.ViewModels.Product;

namespace AIO.Services.Data.Interfaces
{
	/// <summary>
	/// Interface for the product service.
	/// </summary>
	public interface IProductService
	{
		/// <summary>
		/// Method from IProductService that returns the first three expiring products.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts();

		/// <summary>
		/// Method from IProductService that creates a product and returns its id.
		/// </summary>
		/// <param name="formModel"></param>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		Task<string> CreateProductAndRerurnIdAsync(ProductFormModel formModel, string sellerId);

		/// <summary>
		/// Method from IProductService that returns all products filtered and paged.
		/// </summary>
		/// <param name="queryModel"></param>
		/// <returns></returns>
		Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsFilteredAndPagedAsync(AllProductsQueryModel queryModel);

		/// <summary>
		/// Method from IProductService that returns all products by seller id.
		/// </summary>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		Task<IEnumerable<ProductAllViewModel>> GetAllProductsBySellerIdAsync(string sellerId);

		/// <summary>
		/// Method from IProductService that returns all products by user id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId);

		/// <summary>
		/// Method from IProductService that returns all products by category id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that checks if a product exists by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task<bool> ExistsByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that returns a product for editing by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task<ProductFormModel> GetProductForEditByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that checks if a seller is the owner of a product by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		Task<bool> IsSellerOwnerOfProductWithIdAsync(string productId, string sellerId);

		/// <summary>
		/// Method from IProductService that edits a product by its id and form model.
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="formModel"></param>
		/// <returns></returns>
		Task EditProductByIdAndFormModel(string productId, ProductFormModel formModel);

		/// <summary>
		/// Method from IProductService that returns a product for delete by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that deletes a product by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task DeleteProductByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that reactivates a product by its id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task ReactivateProductByIdAsync(string productId);

		/// <summary>
		/// Method from IProductService that checks if a product is expired.
		/// </summary>
		/// <returns></returns>
		Task CheckProductIfItIsExpiredAsync();

		/// <summary>
		/// Method from IProductService that returns the seller full name by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		Task<string> GetSellerFullNameByProductIdAsync(string productId);
	}
}
