using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;

namespace AIO.Services.Tests
{
	[TestFixture]
	public class ProductCategoryServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private IProductCategoryService productCategoryService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();

			List<Category> categories = dbContext.Categories.ToList();

			SeedDatabase(dbContext);

			productCategoryService = new ProductCategoryService(dbContext);
		}

		[Test]
		public async Task GetAllProductCategoriesAsyncShouldReturnAllProductCategories()
		{
			ICollection<ProductCategoryViewModel> productCategories = await this.productCategoryService.GetAllProductCategoriesAsync();

			Assert.That(productCategories.Count(), Is.EqualTo(3));
		}

		[Test]
		public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
		{
			bool result = await this.productCategoryService.ExistsByIdAsync(1);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ExistsByIdAsyncShouldReturnFalseWhenNotExists()
		{
			bool result = await this.productCategoryService.ExistsByIdAsync(4);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task AllProductCategoryNamesAsyncShouldReturnAllProductCategoryNames()
		{
			IEnumerable<string> allProductCategoryNames = await this.productCategoryService.AllProductCategoryNamesAsync();

			Assert.That(allProductCategoryNames.Count(), Is.EqualTo(3));
		}

		[Test]
		public async Task AllProductCategoryNamesAsyncShouldReturnAllProductCategoryNamesCorrectly()
		{
			IEnumerable<string> allProductCategoryNames = await this.productCategoryService.AllProductCategoryNamesAsync();

			Assert.That(allProductCategoryNames.First(), Is.EqualTo("Vehicle"));
			Assert.That(allProductCategoryNames.Skip(1).First(), Is.EqualTo("Bicycle"));
			Assert.That(allProductCategoryNames.Last(), Is.EqualTo("Real Estate"));
		}

		[Test]
		public async Task ExistsByNameAsyncShouldReturnTrueWhenExists()
		{
			bool result = await this.productCategoryService.ExistsByNameAsync("Vehicle");

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ExistsByNameAsyncShouldReturnFalseWhenNotExists()
		{
			bool result = await this.productCategoryService.ExistsByNameAsync("NotExists");

			Assert.IsFalse(result);
		}

		[Test]
		public async Task ExistsByNameAsyncShouldReturnFalseWhenExistsButDifferentCase()
		{
			bool result = await this.productCategoryService.ExistsByNameAsync("vehicle");

			Assert.IsFalse(result);
		}

		[Test]
		public async Task ExistsByNameAsyncShouldReturnTrueWhenExistsWithDifferentCase()
		{
			bool result = await this.productCategoryService.ExistsByNameAsync("Vehicle");

			Assert.IsTrue(result);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}
