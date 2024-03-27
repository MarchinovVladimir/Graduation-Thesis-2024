using AIO.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.LocatiomArea;
using Microsoft.EntityFrameworkCore;

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
	}
}
