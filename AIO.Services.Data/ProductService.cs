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
				.Where(p => p.IsActive)
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
				.Where(p => p.IsActive)
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

		public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsByAgentIdAsync(string agentId)
		{
			IEnumerable<ProductAllViewModel> allAgentProducts = await this.dbContext
				.Products
				.AsNoTracking()
				.Where(p => p.IsActive && 
							p.AgentId.ToString() == agentId)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.OpeningBid,
				}).ToArrayAsync();

			return allAgentProducts;
		}

		public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId)
		{
			IEnumerable<ProductAllViewModel> allUserProducts =await  this.dbContext
				.Products
				.AsNoTracking()
				.Where(p => p.IsActive && 
							p.BuyerId.ToString() == userId)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.OpeningBid,
				}).ToArrayAsync();

			return allUserProducts;
		}

		
		public async Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(string productId)
		{
			Product? product = await dbContext
				.Products
				.Include(p => p.Category)
				.Include(p => p.Agent)
				.ThenInclude(a => a.User)
				.AsNoTracking()
				.Where(p => p.IsActive)
				.FirstOrDefaultAsync(p => p.Id.ToString() == productId);

			if (product == null)
			{
				return null;
			}

			return new ProductDetailsViewModel()
			{ 
				Id= product.Id.ToString(),
				Title = product.Title,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Category = product.Category.Name,
				Price  = product.CurrentBid,
				Agent = new Web.ViewModels.Agent.AgentInfoOnProductViewModel()
				{
					Email = product.Agent.User.Email,
					PhoneNumber = product.Agent.PhoneNumber
				}
				
			};
				
		}
	}
}
