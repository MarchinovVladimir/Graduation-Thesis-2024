using AIO.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly AIODbContext context;

		public ProductCategoryService(AIODbContext context)
		{
			this.context = context;
		}

		public async Task<ICollection<ProductCategoryViewModel>> GetAllProductCategoriesAsync()
		{
			ICollection<ProductCategoryViewModel> productCategories = await this.context.Categories
				.AsNoTracking()
				.Select(c => new ProductCategoryViewModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

				return productCategories;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await this.context.Categories.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
