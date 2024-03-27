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
	/// <summary>
	/// Service class for products.
	/// </summary>
	public class ProductService : IProductService
	{
		private readonly AIODbContext dbContext;

		public ProductService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Service method for getting the first three expiring products.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts()
		{
			IEnumerable<ProductIndexViewModel> firstThreeExpireProducts = await this.dbContext.Products
				.AsNoTracking()
				.Where(p => p.IsActive)
				.OrderByDescending(p => p.ExpirationDate)
				.Take(3)
				.Select(p => new ProductIndexViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
				}).ToArrayAsync();

			return firstThreeExpireProducts;
		}

		/// <summary>
		/// Service method for creating a product and returning its id.
		/// </summary>
		/// <param name="formModel"></param>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		public async Task<string> CreateProductAndRerurnIdAsync(ProductFormModel formModel, string sellerId)
		{
			Product product = new Product
			{
				Title = formModel.Title,
				Description = formModel.Description,
				ImageUrl = formModel.ImageUrl,
				Price = formModel.Price,
				CategoryId = formModel.CategoryId,
				SellerId = Guid.Parse(sellerId),
			};

			await this.dbContext.Products.AddAsync(product);
			await this.dbContext.SaveChangesAsync();

			return product.Id.ToString();
		}

		/// <summary>
		/// Service method for getting all products filtered and paged.
		/// </summary>
		/// <param name="queryModel"></param>
		/// <returns></returns>
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
				ProductSorting.Newest => productsQuery
				.OrderByDescending(p => p.ExpirationDate),
				ProductSorting.Oldest => productsQuery.OrderBy(p => p.CreatedOn),
				ProductSorting.PriceLowToHigh => productsQuery.OrderBy(p => p.Price),
				ProductSorting.PriceHighToLow => productsQuery
				.OrderByDescending(p => p.Price),
				_ => productsQuery.OrderByDescending(p => p.ExpirationDate),
			};

			IEnumerable<ProductAllViewModel> allProducts = await productsQuery
				.Where(p => !p.IsSold)
				.Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
				.Take(queryModel.ProductsPerPage)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.Price,	
					IsActive = p.IsActive,
					IsSold = p.IsSold
				}).ToArrayAsync();	

			int totalProductsCount = productsQuery.Count();

			return new AllProductsFilteredAndPagedServiceModel 
			{ 
				TotalProductsCount = totalProductsCount,
				Products = allProducts
			};
		}

		/// <summary>
		/// Service method for getting all products by seller id.
		/// </summary>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsBySellerIdAsync(string sellerId)
		{
			IEnumerable<ProductAllViewModel> allSellerProducts = await this.dbContext
				.Products
				.AsNoTracking()
				.Where(p => !p.IsSold && 
							p.SellerId.ToString() == sellerId)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.Price,
					IsActive = p.IsActive,
					IsSold = p.IsSold
				}).ToArrayAsync();

			return allSellerProducts;
		}

		/// <summary>
		/// Service method for getting all products by user id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId)
		{
			IEnumerable<ProductAllViewModel> allUserProducts =await  this.dbContext
				.Products
				.AsNoTracking()
				.Where(p => !p.IsSold && 
							p.BuyerId.ToString() == userId)
				.Select(p => new ProductAllViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
					Price = p.Price,
					IsActive = p.IsActive,
					IsSold = p.IsSold
				}).ToArrayAsync();

			return allUserProducts;
		}

		/// <summary>
		/// Service method for getting product details by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Include(p => p.Category)
				.Include(p => p.Seller)
				.ThenInclude(a => a.User)
				.AsNoTracking()
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);	

			return new ProductDetailsViewModel()
			{ 
				Id= product.Id.ToString(),
				Title = product.Title,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Category = product.Category.Name,
				Price  = product.Price,
				CreatedOn = product.CreatedOn.ToString("dd/MM/yyyy"),
				ExpirationDate = product.ExpirationDate.ToString("dd/MM/yyyy"),
				Seller = new Web.ViewModels.Seller.SellerInfoOnProductViewModel()
				{
					Email = product.Seller.User.Email,
					PhoneNumber = product.Seller.PhoneNumber
				}
				
			};
				
		}

		/// <summary>
		/// Service method for checking if product exists by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task<bool> ExistsByIdAsync(string productId)
		{
			return await dbContext.Products.AnyAsync(p => p.Id.ToString() == productId);
		}

		/// <summary>
		/// Service method for getting product form by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Service method for checking if seller is owner of product with product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		public async Task<bool> IsSellerOwnerOfProductWithIdAsync(string productId, string sellerId)
		{
			Product product = await dbContext.Products
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);

			return product.SellerId.ToString() == sellerId;
		}

		/// <summary>
		/// Service method for editing product by product id and product form model.
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="formModel"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Service method for getting product for delete by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);

			return new ProductPreDeleteDetailsViewModel()
			{
				Title = product.Title,
				Description = product.Description,
				ImageUrl = product.ImageUrl
			};
		}

		/// <summary>
		/// Service method for soft deleting product by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task DeleteProductByIdAsync(string productId)
		{
			Product productToDelete = await dbContext
				.Products
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);

			productToDelete.IsSold = true;
			productToDelete.IsActive = false;

			await dbContext.SaveChangesAsync();
		}

		public async Task ReactivateProductByIdAsync(string productId)
		{
			Product productToReactivate = await dbContext
				.Products
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);

			productToReactivate.IsActive = true;
			productToReactivate.CreatedOn = DateTime.UtcNow;

			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Service method for getting seller full name by product id.
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task<string> GetSellerFullNameByProductIdAsync(string productId)
		{
			Product product = await dbContext
				.Products
				.Include(p => p.Seller)
				.ThenInclude(a => a.User)
				.Where(p => !p.IsSold)
				.FirstAsync(p => p.Id.ToString() == productId);

			return $"{product.Seller.User.FirstName} {product.Seller.User.LastName}";
		}

		/// <summary>
		/// Service method for checking if product is expired.
		/// </summary>
		/// <returns></returns>
		public async Task CheckProductIfItIsExpired()
		{
				await dbContext
				.Products
				.Where(p => p.IsActive && p.ExpirationDate < DateTime.UtcNow)
				.ForEachAsync(p => p.IsActive = false);

			await dbContext.SaveChangesAsync();
		}
	}
}
