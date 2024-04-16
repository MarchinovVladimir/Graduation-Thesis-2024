using AIO.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.LocationArea;
using Microsoft.EntityFrameworkCore;
using AIO.Areas.Admin.ViewModels;
using AIO.Web.ViewModels.Product;
using AIO.Data.Models;

namespace AIO.Services.Data
{
	public class LocationAreaService : ILocationAreaService
	{
		private readonly AIODbContext dbContext;

		public LocationAreaService(AIODbContext _dbContext)
		{
			dbContext = _dbContext;

		}

		public async Task<ICollection<LocationAreaViewModel>> GetAllLocationAreasAsync()
		{
			return await dbContext.LocationAreas
				.Select(la => new LocationAreaViewModel
				{
					Id = la.Id,
					Name = la.Name,
					PostCode = la.PostCode
				})
				.ToListAsync();
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			return await dbContext.LocationAreas.AnyAsync(la => la.Id == id);
		}

		public async Task<IEnumerable<string>> AllLocationAreasNamesAsync()
		{
			return await dbContext.LocationAreas
				.Select(la => la.Name)
				.ToArrayAsync();
		}

		public async Task<LocationAreaFormModel> GetLocationAreaByIdAsync(int id)
		{
			return await dbContext.LocationAreas
				.Where(la => la.Id == id)
				.Select(la => new LocationAreaFormModel
				{
					Name = la.Name,
					PostCode = la.PostCode
				})
				.FirstAsync();
		}

		public async Task EditLocationAreaByIdAndFormModel(int locationAreaId, LocationAreaFormModel formModel)
		{
			LocationArea locationArea = await dbContext
				.LocationAreas
				.FirstAsync(la => la.Id == locationAreaId);

			locationArea.Name = formModel.Name;
			locationArea.PostCode = formModel.PostCode;

			await dbContext.SaveChangesAsync();
		}

		public async Task AddLocationAreaAsync(LocationAreaFormModel locationArea)
		{
			LocationArea newLocationArea = new LocationArea
			{
				Name = locationArea.Name,
				PostCode = locationArea.PostCode
			};

			await dbContext.LocationAreas.AddAsync(newLocationArea);
			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> ExistsByNameAsync(string name)
		{
			return  await dbContext.LocationAreas.AnyAsync(la => la.Name == name);
		}
	}
}
