using AIO.Data;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;


namespace AIO.Services.Tests
{
	public class AgentServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private IAgentService agentService;


		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			agentService = new AgentService(dbContext);
		}	

		[Test]
		public async Task IsAgentExistByUserIdAsyncShouldRetrunTrueWhenExists()
		{
			string existingAgentUserId = AgentUser!.Id.ToString();

			bool result = await this.agentService.IsSellerExistByUserIdAsync(existingAgentUserId);

			Assert.IsTrue(result);

		}

		[Test]
		public async Task AgentExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
		{
			string existingAgentUserId = Agent!.Id.ToString();

			bool result = await this.agentService.IsSellerExistByUserIdAsync(existingAgentUserId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsAgentExistByPhoneNumberAsyncShouldRetrunTrueWhenExists()
		{
			string existingAgentPhoneNumber = Agent!.PhoneNumber;

			bool result = await this.agentService.IsSellerExistByPhoneNumberAsync(existingAgentPhoneNumber);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsAgentExistByPhoneNumberAsyncShouldReturnFalseWhenNotExists()
		{
			string existingAgentPhoneNumber = "1234567890";

			bool result = await this.agentService.IsSellerExistByPhoneNumberAsync(existingAgentPhoneNumber);

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