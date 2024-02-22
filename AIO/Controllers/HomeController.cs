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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}