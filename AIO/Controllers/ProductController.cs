using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants; 

namespace AIO.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductCategoryService productCategoryService;
		private readonly IAgentService agentService;

		public ProductController(IProductCategoryService productCategoryService, IAgentService agentService)
		{
			this.productCategoryService = productCategoryService;
			this.agentService = agentService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{

			bool isAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());

			if (!isAgent)
			{
				TempData[ErrorMessage] = "You must become an agent to add products.";

				return RedirectToAction("Become", "Agent");
			}

			AddProductFormModel model = new AddProductFormModel()
			{
				Categories = await productCategoryService.GetAllProductCategoriesAsync()
			};
			return View(model);
		}
	}
}
