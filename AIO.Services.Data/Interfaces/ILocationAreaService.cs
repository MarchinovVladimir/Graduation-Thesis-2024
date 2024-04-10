using AIO.Areas.Admin.ViewModels;
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

		/// <summary>
		/// Method that returns a location area by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<LocationAreaFormModel> GetLocationAreaByIdAsync(int id);

		/// <summary>
		/// Method that edits a location area by its id and form model.
		/// </summary>
		/// <param name="locationAreaId"></param>
		/// <param name="formModel"></param>
		/// <returns></returns>
		Task EditLocationAreaByIdAndFormModel(int locationAreaId, LocationAreaFormModel formModel);

		/// <summary>
		/// Method that adds a location area.
		/// </summary>
		/// <param name="locationArea"></param>
		/// <returns></returns>
		Task AddLocationAreaAsync(LocationAreaFormModel locationArea);

		/// <summary>
		/// Method that checks if a location area exists by its name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		Task<bool> ExistsByNameAsync(string name);
	}
}
