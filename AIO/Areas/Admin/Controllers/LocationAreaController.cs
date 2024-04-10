using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.LocationArea;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants;
using static AIOCommon.ErrorMessageConstants.LocationArea;
using static AIOCommon.GeneralAppConstants;
using static AIOCommon.InformationalMessagesConstants.LocationArea;
using AIO.Areas.Admin.ViewModels;

namespace AIO.Areas.Admin.Controllers
{
	/// <summary>
	/// Location area controller for admin area.
	/// </summary>
	public class LocationAreaController : BaseAdminController
	{
		private readonly ILocationAreaService locationAreaService;

		public LocationAreaController(ILocationAreaService locationAreaService)
		{
			this.locationAreaService = locationAreaService;
		}

		/// <summary>
		/// Get all location areas.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> All()
		{
			try
			{

				ICollection<LocationAreaViewModel> locationAreas = await locationAreaService.GetAllLocationAreasAsync();

				return View(locationAreas);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Add location area get action.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Add()
		{
			LocationAreaFormModel locationArea = new LocationAreaFormModel();
			return View(locationArea);
		}

		/// <summary>
		/// Add location area post action.
		/// </summary>
		/// <param name="locationArea"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add(LocationAreaFormModel locationArea)
		{
			if (await locationAreaService.ExistsByNameAsync(locationArea.Name))
			{
				ModelState.AddModelError(nameof(locationArea.Name), LocationAreaExistsErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				return View(locationArea);
			}

			try
			{
				await locationAreaService.AddLocationAreaAsync(locationArea);
				TempData[SuccessMessage] = SuccessfullyAddedLocationAreaMessage;

				return RedirectToAction("All", "LocationArea", new {area = AdminAreaName});
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Edit location area get action.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				LocationAreaFormModel locationArea = await locationAreaService.GetLocationAreaByIdAsync(id);

				if (locationArea == null)
				{
					TempData[ErrorMessage] = LocationAreaNotFoundMessage;

					return RedirectToAction("All", "LocationArea", new {area = AdminAreaName});
				}

				return View(locationArea);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Edit location area post action.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="locationArea"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(int id, LocationAreaFormModel locationArea)
		{
			if (!ModelState.IsValid)
			{
				return View(locationArea);
			}

			try
			{
				await locationAreaService.EditLocationAreaByIdAndFormModel(id, locationArea);
				return RedirectToAction("All", "LocationArea", new {area = AdminAreaName});
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = GeneralErrorMessage;

			return RedirectToAction("Index", "Home", new {area = AdminAreaName});
		}
	}
}
