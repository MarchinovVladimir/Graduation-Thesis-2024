using AIO.Data;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;

namespace AIO.Services.Tests
{
	[TestFixture]
	public class UserServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private IUserService userService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			userService = new UserService(dbContext);
		}

		[Test]
		public async Task GetFullNameByEmailAsyncShouldReturnFullName()
		{
			string email = SellerUser!.Email;

			string result = await this.userService.GetFullNameByEmailAsync(email);

			Assert.That($"{SellerUser.FirstName} {SellerUser.LastName}", Is.EqualTo(result));
		}

		[Test]
		public async Task GetFullNameByEmailAsyncShouldReturnEmptyStringWhenUserNotExists()
		{
			string email = "nonExistingEmail";

			string result = await this.userService.GetFullNameByEmailAsync(email);

			Assert.That(string.Empty, Is.EqualTo(result));
		}

		[Test]
		public async Task GetFullNameByIdAsyncShouldReturnFullName()
		{
			string userId = SellerUser!.Id.ToString();

			string result = await this.userService.GetFullNameByIdAsync(userId);

			Assert.That($"{SellerUser.FirstName} {SellerUser.LastName}", Is.EqualTo(result));
		}

		[Test]
		public async Task GetFullNameByIdAsyncShouldReturnEmptyStringWhenUserNotExists()
		{
			string userId = "nonExistingUserId";

			string result = await this.userService.GetFullNameByIdAsync(userId);

			Assert.That(string.Empty, Is.EqualTo(result));
		}

		[Test]
		public async Task AllAsyncShouldReturnAllUsers()
		{
			List<UserViewModel> allUsers = (await this.userService.AllAsync()).ToList();

			Assert.That(allUsers.Count, Is.EqualTo(2));
			Assert.That(allUsers[0].Email, Is.EqualTo(SellerUser.Email));
			Assert.That(allUsers[0].FullName, Is.EqualTo($"{SellerUser.FirstName} {SellerUser.LastName}"));
			Assert.That(allUsers[1].Email, Is.EqualTo(User!.Email));
			Assert.That(allUsers[1].FullName, Is.EqualTo($"{User!.FirstName} {User!.LastName}"));
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}

	}
}
