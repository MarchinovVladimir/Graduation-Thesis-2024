using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIO.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
           this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductIndexViewModel> viewModel = await productService.GetFirstThreeExpiringProducts();

            return View(viewModel);
        }

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