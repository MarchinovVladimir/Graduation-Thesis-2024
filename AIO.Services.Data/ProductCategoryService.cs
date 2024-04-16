using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	/// <summary>
	/// Product category service.
	/// </summary>
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly AIODbContext dbContext;

		public ProductCategoryService(AIODbContext context)
		{
			this.dbContext = context;
		}

		/// <summary>
		/// Service method that returns all product categories.
		/// </summary>
		/// <returns></returns>
		public async Task<ICollection<ProductCategoryViewModel>> GetAllProductCategoriesAsync()
		{
			ICollection<ProductCategoryViewModel> productCategories = await	  dbContext.Categories
				.AsNoTracking()
				.Select(c => new ProductCategoryViewModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

				return productCategories;
		}

		/// <summary>
		/// Service method that checks if a product category exists by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await this.dbContext.Categories.AnyAsync(c => c.Id == id);

			return result;
		}

		/// <summary>
		/// Service method that returns all product category names.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<string>> AllProductCategoryNamesAsync()
		{
			IEnumerable<string> AllProductCategoryNames = await this.dbContext.Categories
				.AsNoTracking()
				.Select(c => c.Name)
				.ToArrayAsync();

			return AllProductCategoryNames;
		}

		/// <summary>
		/// Service method that checks if a product category exists by its name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<bool> ExistsByNameAsync(string name)
		{
			bool result = await dbContext.Categories.AnyAsync(c => c.Name == name);

			return result;
		}

		/// <summary>
		/// Service method that adds a product category.
		/// </summary>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		public async Task AddProductCategoryAsync(ProductCategoryFormModel productCategory)
		{
			Category newCategory = new Category
			{
				Name = productCategory.Name
			};

			await dbContext.Categories.AddAsync(newCategory);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Service method that gets a product category by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<ProductCategoryFormModel> GetProductCategoryByIdAsync(int id)
		{
			return await dbContext.Categories
				.Where(c => c.Id == id)
				.Select(c => new ProductCategoryFormModel
				{
					Name = c.Name
				})
				.FirstAsync();
		}

		/// <summary>
		/// Service method that gets a product category by id and edits it.
		/// </summary>
		/// <param name="productCategoryId"></param>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		public async Task EditProductCategoryAsync(int productCategoryId, ProductCategoryFormModel productCategory)
		{
			Category category = await dbContext
				.Categories
				.FirstAsync(c => c.Id == productCategoryId);

			category.Name = productCategory.Name;

			await dbContext.SaveChangesAsync();
		}
	}
}
