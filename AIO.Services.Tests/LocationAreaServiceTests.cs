using AIO.Data;
using AIO.Services.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.LocationArea;
using Microsoft.EntityFrameworkCore;
using static AIO.Services.Tests.DatabaseSeeder;

namespace AIO.Services.Tests
{
	[TestFixture]
	public class LocationAreaServiceTests
	{
		private DbContextOptions<AIODbContext> dbOptions;
		private AIODbContext dbContext;

		private ILocationAreaService locationAreaService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<AIODbContext>()
				.UseInMemoryDatabase("AIOInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new AIODbContext(dbOptions);

			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);

			locationAreaService = new LocationAreaService(dbContext);
		}

		[Test]
		public async Task GetAllLocationAreasAsyncShouldReturnAllLocationAreas()
		{
			ICollection<LocationAreaViewModel> locationAreas = await this.locationAreaService.GetAllLocationAreasAsync();

			Assert.AreEqual(4, locationAreas.Count);
		}

		[Test]
		public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
		{
			bool result = await this.locationAreaService.ExistsByIdAsync(1);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ExistsByIdAsyncShouldReturnFalseWhenNotExists()
		{
			bool result = await this.locationAreaService.ExistsByIdAsync(10);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task AllLocationAreasNamesAsyncShouldReturnAllLocationAreasNames()
		{
			IEnumerable<string> locationAreasNames = await this.locationAreaService.AllLocationAreasNamesAsync();

			Assert.AreEqual(4, locationAreasNames.Count());
		}

		[Test]
		public async Task AllLocationAreasNamesAsyncShouldReturnAllLocationAreasNamesCorrectly()
		{
			IEnumerable<string> locationAreasNames = await this.locationAreaService.AllLocationAreasNamesAsync();

			Assert.AreEqual("Sofia", locationAreasNames.First());
			Assert.AreEqual("Plovdiv", locationAreasNames.Skip(1).First());
			Assert.AreEqual("Varna", locationAreasNames.Skip(2).First());
			Assert.AreEqual("Burgas", locationAreasNames.Skip(3).First());
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}
