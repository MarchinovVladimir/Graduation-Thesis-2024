using AIO.Data.Models;

namespace AIO.Services.Data.Interfaces
{
	public interface IProductCategoryService
	{
		Task<IEnumerable<Category>> GetAllAsync();
	}
}
