using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Services.Data.Models.Product;
using AIO.Views.Product.Enums;
using AIO.Web.ViewModels.Home;
using AIO.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	public class ProductService : IProductService
	{
		private readonly AIODbContext dbContext;

		public ProductService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts()
		{
			IEnumerable<ProductIndexViewModel> firstThreeExpireProducts = await this.dbContext.Products
				.AsNoTracking()
				.Where(p => p.IsAuctionClosed == false)
				.OrderBy(p => p.EndTime)
				.Take(3)
				.Select(p => new ProductIndexViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
				}).ToArrayAsync();

			return firstThreeExpireProducts;
		}

		public async Task CreateProductAsync(AddProductFormModel formModel, string agentId)
		{
			Product product = new Product
			{
				Title = formModel.Title,
				Description = formModel.Description,
				ImageUrl = formModel.ImageUrl,
				OpeningBid = formModel.OpeningBid,
				CategoryId = formModel.CategoryId,
				AgentId = Guid.Parse(agentId),
			};

			await this.dbContext.Products.AddAsync(product);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsFilteredAndPagedAsync(AllProductsQueryModel queryModel)
		{
			IQueryable<Product> productsQuery = this.dbContext
				.Products
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryModel.Category))
			{
				productsQuery = productsQuery
										.Where(p => p.Category.Name == queryModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
			{
				string wildCard = $"%{queryModel.SearchString.ToLower()}%";	
				productsQuery = productsQuery
										.Where(p => EF.Functions.Like(p.Title, wildCard) ||
													EF.Functions.Like(p.Description, wildCard));
			}

			productsQuery = queryModel.ProductSorting switch
			{
				ProductSorting.Newest => productsQuery.OrderByDescending(p => p.StartTime),
				ProductSorting.Oldest => productsQuery.OrderBy(p => p.StartTime),
				ProductSorting.PriceLowToHigh => productsQuery.OrderBy(p => p.OpeningBid),
				ProductSorting.PriceHighToLow => productsQuery.OrderByDescending(p => p.OpeningBid),
				_ => productsQuery.OrderByDescending(p => p.StartTime),
			};

			IEnumerable<ProductAllViewModel> allProducts = await productsQuery
				.Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
				.Take(queryModel.ProductsPerPage)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.OpeningBid,	
				}).ToArrayAsync();	

			int totalProductsCount = await productsQuery.CountAsync();

			return new AllProductsFilteredAndPagedServiceModel 
			{ 
				TotalProductsCount = totalProductsCount,
				Products = allProducts
			};
		}
	}
}
