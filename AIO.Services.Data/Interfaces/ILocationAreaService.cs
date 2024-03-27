using AIO.Web.ViewModels.LocationArea;

namespace AIO.Services.Data.Interfaces
{
	/// <summary>
	/// Location area service interface.
	/// </summary>
	public interface ILocationAreaService
	{
		/// <summary>
		/// Method that returns all location areas.
		/// </summary>
		/// <returns></returns>
		Task<ICollection<LocationAreaViewModel>> GetAllLocationAreasAsync();

		/// <summary>
		/// Method that checks if a location area exists by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<bool> ExistsByIdAsync(int id);

		/// <summary>
		/// Method that returns all location areas names.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<string>> AllLocationAreasNamesAsync();
	}
}
