using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIO.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return View();
		}
	}
}
