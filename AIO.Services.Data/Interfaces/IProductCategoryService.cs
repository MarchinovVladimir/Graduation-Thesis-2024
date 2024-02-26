using AIO.Web.ViewModels.ProductCategory;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductCategoryService
	{
		Task<ICollection<ProductCategoryViewModel>> GetAllProductCategoriesAsync();
	}
}
