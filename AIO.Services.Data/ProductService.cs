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

		public async Task<string> CreateProductAndRerurnIdAsync(ProductFormModel formModel, string agentId)
		{
			Product product = new Product
			{
				Title = formModel.Title,
				Description = formModel.Description,
				ImageUrl = formModel.ImageUrl,
				Price = formModel.Price,
				CategoryId = formModel.CategoryId,
				AgentId = Guid.Parse(agentId),
			};

			await this.dbContext.Products.AddAsync(product);
			await this.dbContext.SaveChangesAsync();

			return product.Id.ToString();
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
				ProductSorting.PriceLowToHigh => productsQuery.OrderBy(p => p.Price),
				ProductSorting.PriceHighToLow => productsQuery.OrderByDescending(p => p.Price),
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
					Price = p.Price,	
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
					Price = p.Price,
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
					Price = p.Price,
				}).ToArrayAsync();

			return allUserProducts;
		}

		
		public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Include(p => p.Category)
				.Include(p => p.Agent)
				.ThenInclude(a => a.User)
				.AsNoTracking()
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);	

			return new ProductDetailsViewModel()
			{ 
				Id= product.Id.ToString(),
				Title = product.Title,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Category = product.Category.Name,
				Price  = product.Price,
				Agent = new Web.ViewModels.Agent.AgentInfoOnProductViewModel()
				{
					Email = product.Agent.User.Email,
					PhoneNumber = product.Agent.PhoneNumber
				}
				
			};
				
		}

		public async Task<bool> ExistsByIdAsync(string productId)
		{
			return await dbContext.Products.AnyAsync(p => p.Id.ToString() == productId);
		}

		public async Task<ProductFormModel> GetProductFormByIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Include(p => p.Category)
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			return new ProductFormModel()
			{
				Title = product.Title,
				Description= product.Description,
				ImageUrl = product.ImageUrl,
				Price = product.Price,
				CategoryId = product.CategoryId,
			};
		}

		public async Task<bool> IsAgentOwnerOfProductWithIdAsync(string productId, string agentId)
		{
			Product product = await dbContext.Products
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			return product.AgentId.ToString() == agentId;
		}

		public async Task EditProductByIdAndFormModel(string productId, ProductFormModel formModel)
		{
			Product product = await dbContext
				.Products
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			product.Title = formModel.Title;
			product.Description = formModel.Description;
			product.ImageUrl = formModel.ImageUrl;
			product.CategoryId = formModel.CategoryId;
			product.Price = formModel.Price;	

			await dbContext.SaveChangesAsync();
		}

		public async Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			return new ProductPreDeleteDetailsViewModel()
			{
				Title = product.Title,
				Description = product.Description,
				ImageUrl = product.ImageUrl
			};
		}

		public async Task DeleteProductByIdAsync(string productId)
		{
			Product productToDelete = await dbContext
				.Products
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			productToDelete.IsActive = false;

			await dbContext.SaveChangesAsync();
		}

		public async Task<string> GetSellerFullNameByProductIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Include(p => p.Agent)
				.ThenInclude(a => a.User)
				.Where(p => p.IsActive)
				.FirstAsync(p => p.Id.ToString() == productId);

			return $"{product.Agent.User.FirstName} {product.Agent.User.LastName}";
		}
	}
}
