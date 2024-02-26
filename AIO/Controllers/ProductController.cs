using AIO.Services.Data.Interfaces;
using AIO.Services.Data.Models.Product;
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
		private readonly IProductService productService;

		public ProductController(IProductCategoryService productCategoryService, IAgentService agentService, IProductService productService)
		{
			this.productCategoryService = productCategoryService;
			this.agentService = agentService;
			this.productService = productService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery]AllProductsQueryModel queryModel)
		{
			AllProductsFilteredAndPagedServiceModel serviceModel = await productService.GetAllProductsFilteredAndPagedAsync(queryModel);

			queryModel.Products = serviceModel.Products;
			queryModel.TotalProducts = serviceModel.TotalProductsCount;
			queryModel.Categories = await this.productCategoryService.AllProductCategoryNamesAsync();

			return this.View(queryModel);
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

		[HttpPost]
		public async Task<IActionResult> Add(AddProductFormModel model)
		{
			bool isAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());

			if (!isAgent)
			{
				TempData[ErrorMessage] = "You must become an agent to add products.";

				return RedirectToAction("Become", "Agent");
			}

			if (!await productCategoryService.ExistsByIdAsync(model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}

			try
			{
				string agentId = await agentService.GetAgentIdByUserId(this.User.GetId());
				await productService.CreateProductAsync(model, agentId);
			}
			catch (Exception _)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while adding the product.");

				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}

			return RedirectToAction(nameof(All));
		}
	}
}
