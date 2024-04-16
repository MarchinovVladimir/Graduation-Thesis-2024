using AIO.Areas.Admin.ViewModels;
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

			Assert.That(locationAreas.Count(), Is.EqualTo(4));
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

			Assert.That(locationAreasNames.Count(), Is.EqualTo(4));
		}

		[Test]
		public async Task AllLocationAreasNamesAsyncShouldReturnAllLocationAreasNamesCorrectly()
		{
			IEnumerable<string> locationAreasNames = await this.locationAreaService.AllLocationAreasNamesAsync();

			Assert.That(locationAreasNames.First(), Is.EqualTo("Sofia"));
			Assert.That(locationAreasNames.Skip(1).First(), Is.EqualTo("Plovdiv"));
			Assert.That(locationAreasNames.Skip(2).First(), Is.EqualTo("Varna"));
			Assert.That(locationAreasNames.Skip(3).First(), Is.EqualTo("Burgas"));
		}

		[Test]
		public async Task GetLocationAreaByIdAsyncShouldReturnCorrectLocationArea()
		{
			LocationAreaFormModel locationArea = 
				await this.locationAreaService.GetLocationAreaByIdAsync(1);

			Assert.That(locationArea.Name, Is.EqualTo("Sofia"));
		}

		[Test]
		public async Task EditLocationAreaByIdAndFormModelShouldEditCorrectly()
		{
			LocationAreaFormModel locationArea = new LocationAreaFormModel
			{
				Name = "Sofia",
				PostCode = "1000"
			};

			await this.locationAreaService.EditLocationAreaByIdAndFormModel(1, locationArea);

			LocationAreaFormModel editedLocationArea = 
				await this.locationAreaService.GetLocationAreaByIdAsync(1);

			Assert.That(editedLocationArea.Name, Is.EqualTo("Sofia"));
			Assert.That(editedLocationArea.PostCode, Is.EqualTo("1000"));
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}
