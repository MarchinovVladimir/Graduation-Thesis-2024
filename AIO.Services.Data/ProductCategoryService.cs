﻿using AIO.Data;
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
		private readonly AIODbContext context;

		public ProductCategoryService(AIODbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Service method that returns all product categories.
		/// </summary>
		/// <returns></returns>
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

		public async Task<IEnumerable<string>> AllProductCategoryNamesAsync()
		{
			IEnumerable<string> AllProductCategoryNames = await this.context.Categories
				.AsNoTracking()
				.Select(c => c.Name)
				.ToArrayAsync();

			return AllProductCategoryNames;
		}
	}
}
