using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Controllers
{
	/// <summary>
	/// Home controller.
	/// </summary>
	public class HomeController : Controller
	{
		private readonly IProductService productService;

		public HomeController(IProductService productService)
		{
			this.productService = productService;
		}

		/// <summary>
		/// Index action method.
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Index()
		{
			if (User.IsInRole(AdminRoleName))
			{
				return RedirectToAction("Index", "Home", new { area = AdminAreaName });
			}

			IEnumerable<ProductIndexViewModel> viewModel = await productService.GetFirstThreeExpiringProducts();

			return View(viewModel);
		}

		/// <summary>
		/// Error action method.
		/// </summary>
		/// <param name="statusCode"></param>
		/// <returns></returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400 || statusCode == 404)
			{
				return View("Error404");
			}

			if (statusCode == 401)
			{
				return View("Error401");
			}

			return View();
		}
	}
}