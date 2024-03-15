using Microsoft.AspNetCore.Mvc;

namespace AIO.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
