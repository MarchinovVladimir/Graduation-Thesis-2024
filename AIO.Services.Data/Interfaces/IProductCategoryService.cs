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
	}
}
