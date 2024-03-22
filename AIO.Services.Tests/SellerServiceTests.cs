using AIO.Data;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;


namespace AIO.Services.Tests
{
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
		public async Task IsellerExistByUserIdAsyncShouldRetrunTrueWhenExists()
		{
			string existingSellerUserId = AgentUser!.Id.ToString();

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

		//[Test]
		//public async Task HasProductWithIdAsyncShouldReturnTrueWhenExists()
		//{
		//	string existingAgentUserId = AgentUser!.Id.ToString();
		//	string existingProductId = Product!.Id.ToString();

		//	bool result = await this.agentService.HasProductWithIdAsync(existingAgentUserId, existingProductId);

		//	Assert.IsTrue(result);
		//}

		//[Test]
		//public async Task HasProductWithIdAsyncShouldReturnFalseWhenNotExists()
		//{
		//	string existingAgentUserId = AgentUser!.Id.ToString();
		//	string existingProductId = User!.Id.ToString();

		//	bool result = await this.agentService.HasProductWithIdAsync(existingAgentUserId, existingProductId);

		//	Assert.IsFalse(result);
		//}
	}
}