using AIO.Web.ViewModels.ProductCategory;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductCategoryService
	{
		/// <summary>
		/// Method that returns all product categories.
		/// </summary>
		/// <returns></returns>
		Task<ICollection<ProductCategoryViewModel>> GetAllProductCategoriesAsync();

		/// <summary>
		/// Method that checks if a product category exists by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<bool> ExistsByIdAsync(int id);

		/// <summary>
		/// Method that returns all product category names.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<string>> AllProductCategoryNamesAsync();

		/// <summary>
		/// Method that checks if a product category exists by its name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		Task<bool> ExistsByNameAsync(string name);

		/// <summary>
		/// Method that adds a product category.
		/// </summary>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		Task AddProductCategoryAsync(ProductCategoryFormModel productCategory);
		/// <summary>
		/// Method that gets a product category by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<ProductCategoryFormModel> GetProductCategoryByIdAsync(int id);

		/// <summary>
		/// Method that edits a product category.
		/// </summary>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		Task EditProductCategoryAsync(int productCategoryId, ProductCategoryFormModel productCategory);
	}
}
