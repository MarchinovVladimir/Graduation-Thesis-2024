using AIO.Web.ViewModels.ProductCategory;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductCategoryService
	{
		Task<ICollection<ProductCategoryViewModel>> GetAllProductCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllProductCategoryNamesAsync();
	}
}
