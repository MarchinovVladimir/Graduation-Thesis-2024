using AIO.Data;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;


namespace AIO.Services.Tests
{
	[TestFixture]
	public class SellerServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private ISellerService sellerService;


		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			sellerService = new SellerService(dbContext);
		}

		[Test]
		public async Task IsSellerExistByUserIdAsyncShouldRetrunTrueWhenExists()
		{
			string existingSellerUserId = SellerUser!.Id.ToString();

			bool result = await this.sellerService.IsSellerExistByUserIdAsync(existingSellerUserId);

			Assert.IsTrue(result);

		}

		[Test]
		public async Task SellerExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
		{
			string existingSellerUserId = Seller!.Id.ToString();

			bool result = await this.sellerService.IsSellerExistByUserIdAsync(existingSellerUserId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsSellerExistByPhoneNumberAsyncShouldRetrunTrueWhenExists()
		{
			string existingSellerPhoneNumber = Seller!.PhoneNumber;

			bool result = await this.sellerService.IsSellerExistByPhoneNumberAsync(existingSellerPhoneNumber);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsSellerExistByPhoneNumberAsyncShouldReturnFalseWhenNotExists()
		{
			string existingAgentPhoneNumber = "1234567890";

			bool result = await this.sellerService.IsSellerExistByPhoneNumberAsync(existingAgentPhoneNumber);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetSellerIdByUserIdAsyncShouldReturnSellerId()
		{
			string existingSellerUserId = SellerUser!.Id.ToString();

			var result = await this.sellerService.GetSellerIdByUserIdAsync(existingSellerUserId);

			Assert.AreEqual(Seller!.Id.ToString(), result);
		}

		[Test]
		public async Task GetSellerIdByUserIdAsyncShouldReturnNullIfMissing()
		{
			string existingSellerUserId = Seller!.Id.ToString();

			var result = await this.sellerService.GetSellerIdByUserIdAsync(existingSellerUserId);

			Assert.IsNull(result);
		}

		[Test]
		public async Task HasProductWithIdAsyncShouldReturnTrueIfProductExists()
		{
			string existingSellerUserId = SellerUser!.Id.ToString();
			string existingProductId = Seller!.ProductsForSell.First().Id.ToString();


			var result = await this.sellerService.HasProductWithIdAsync(existingSellerUserId, existingProductId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task HasProductWithIdAsyncShouldReturnFalseIfProductNotExists()
		{
			string existingSellerUserId = SellerUser!.Id.ToString();
			string existingProductId = Guid.NewGuid().ToString();

			var result = await this.sellerService.HasProductWithIdAsync(existingSellerUserId, existingProductId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task CreateAsyncShouldCreateNewSeller()
		{
			string userId = Guid.NewGuid().ToString();
			string phoneNumber = "1234567890";

			await this.sellerService.CreateAsync(userId, new BecomeSellerFormModel
			{
				PhoneNumber = phoneNumber
			});

			var result = await this.sellerService.GetSellerIdByUserIdAsync(userId);

			Assert.IsNotNull(result);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}