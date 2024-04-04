using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;

namespace AIO.Services.Tests
{
	[TestFixture]
	public class ProductServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private IProductService productService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			productService = new ProductService(dbContext);
		}

		[Test]
		public async Task IsProductExistByIdAsyncShouldRetrunTrueWhenExists()
		{
			string existingProductId = dbContext.Products.First().Id.ToString();

			bool result = await this.productService.ExistsByIdAsync(existingProductId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsProductExistByIdAsyncShouldReturnFalseWhenNotExists()
		{
			string existingProductId = "nonExistingProductId";

			bool result = await this.productService.ExistsByIdAsync(existingProductId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task CreateProductAndRerurnIdAsyncShouldCreateProduct()
		{
			ProductFormModel productFormModel = new ProductFormModel()
			{
				Title = "TestProduct",
				Description = "TestProductDescription",
				ImageUrl = "TestProductImageUrl",
				Price = 100,
				CategoryId = 1,
				LocationAreaId = 1
			};

			string sellerId = DatabaseSeeder.Seller!.Id.ToString();

			string productId = await this.productService.CreateProductAndRerurnIdAsync(productFormModel, sellerId);

			Assert.IsNotNull(productId);
		}

		[Test]
		public async Task GetProductDetailsByIdAsyncShouldReturnProductDetails()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			ProductDetailsViewModel productDetails = await this.productService.GetProductDetailsByIdAsync(productId);

			Assert.That(productDetails.Description, Is.EqualTo(DatabaseSeeder.Product!.Description));
			Assert.That(productDetails.Title, Is.EqualTo(DatabaseSeeder.Product!.Title));
			Assert.That(productDetails.Price, Is.EqualTo(DatabaseSeeder.Product!.Price));
		}

		[Test]
		public async Task GetProductForEditByIdAsyncReturnProductForEdit()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			ProductFormModel productFormModel = await this.productService.GetProductForEditByIdAsync(productId);

			Assert.That(productFormModel.Description, Is.EqualTo(DatabaseSeeder.Product!.Description));
			Assert.That(productFormModel.Title, Is.EqualTo(DatabaseSeeder.Product!.Title));
			Assert.That(productFormModel.Price, Is.EqualTo(DatabaseSeeder.Product!.Price));
		}

		[Test]
		public async Task IsSellerOwnerOfProductWithIdAsyncShouldReturnTrueIfSellerIsOwner()
		{
			string sellerId = DatabaseSeeder.Seller!.Id.ToString();
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			bool result = await this.productService.IsSellerOwnerOfProductWithIdAsync(productId, sellerId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsSellerOwnerOfProductWithIdAsyncShouldReturnFalseIfSellerIsNotOwner()
		{
			string sellerId = DatabaseSeeder.User!.Id.ToString();
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			bool result = await this.productService.IsSellerOwnerOfProductWithIdAsync(productId, sellerId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task EditProductAsyncShouldEditProduct()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			ProductFormModel productFormModel = new ProductFormModel()
			{
				Title = "EditedProduct",
				Description = "EditedProductDescription",
				ImageUrl = "https://www.investopedia.com/thmb/DrTUqTdioD2ZougTouHbhrr9ho8=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Term-Definitions_Product-Line-Final-58870113a3ca4770a85cabf3549894bb.jpg",
				Price = 200,
				CategoryId = 1,
				LocationAreaId = 1
			};

			await this.productService.EditProductByIdAndFormModel(productId, productFormModel);

			Product product = dbContext.Products.Find(Guid.Parse(productId))!;

			Assert.That(product.Title, Is.EqualTo(productFormModel.Title));
			Assert.That(product.Description, Is.EqualTo(productFormModel.Description));
			Assert.That(product.ImageUrl, Is.EqualTo(productFormModel.ImageUrl));
			Assert.That(product.Price, Is.EqualTo(productFormModel.Price));
			Assert.That(product.CategoryId, Is.EqualTo(productFormModel.CategoryId));
			Assert.That(product.LocationAreaId, Is.EqualTo(productFormModel.LocationAreaId));
		}

		[Test]
		public async Task GetProductForDeleteByIdAsyncShouldReturnProductForDelete()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			ProductPreDeleteDetailsViewModel productPredeleteFormModel =
				await this.productService.GetProductForDeleteByIdAsync(productId);

			Assert.That(productPredeleteFormModel.Description, Is.EqualTo(DatabaseSeeder.Product!.Description));
			Assert.That(productPredeleteFormModel.Title, Is.EqualTo(DatabaseSeeder.Product.Title));
			Assert.That(productPredeleteFormModel.ImageUrl, Is.EqualTo(DatabaseSeeder.Product.ImageUrl));
		}

		[Test]
		public async Task ReactivateProductByIdAsync()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();
			Product product = dbContext.Products.Find(Guid.Parse(productId))!;
			product.IsActive = false;

			await this.productService.ReactivateProductByIdAsync(productId);


			Assert.That(product.IsActive, Is.True);
			Assert.That(product.IsSold, Is.False);
		}

		[Test]
		public async Task GetSellerFullNameByProductIdAsyncShouldReturnSellerFullName()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			string sellerFullName = await this.productService.GetSellerFullNameByProductIdAsync(productId);

			Assert.That(sellerFullName, Is.EqualTo(DatabaseSeeder.Seller!.User!.FirstName + " " + DatabaseSeeder.Seller!.User!.LastName));
		}

		[Test]
		public async Task GetAllProductsBySellerIdAsyncShouldReturnAllProductsBySellerId()
		{
			string sellerId = DatabaseSeeder.Seller!.Id.ToString();

			IEnumerable<ProductAllViewModel> products 
				= await this.productService.GetAllProductsBySellerIdAsync(sellerId);

			Assert.That(products.Count(), Is.EqualTo(DatabaseSeeder.Seller!.ProductsForSell.Count));
			Assert.That(products.First().Title, Is.EqualTo(DatabaseSeeder.Product!.Title));
			Assert.That(products.First().ImageUrl, Is.EqualTo(DatabaseSeeder.Product!.ImageUrl));
			Assert.That(products.First().Price, Is.EqualTo(DatabaseSeeder.Product!.Price));
			Assert.That(products.First().IsActive, Is.EqualTo(DatabaseSeeder.Product!.IsActive));
			Assert.That(products.First().IsSold, Is.EqualTo(DatabaseSeeder.Product!.IsSold));
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}

		[SetUp]
		public void Setup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			productService = new ProductService(dbContext);
		}

		[Test]
		public async Task DeleteProductByIdAsyncShouldDeleteProductShouldSetIsActiveToFlaseAndIsSoldToTrue()
		{
			string productId = DatabaseSeeder.Seller!.ProductsForSell.First().Id.ToString();

			await this.productService.DeleteProductByIdAsync(productId);

			Product product = dbContext.Products.Find(Guid.Parse(productId))!;

			Assert.That(product.IsActive, Is.False);
			Assert.That(product.IsSold, Is.True);
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}
