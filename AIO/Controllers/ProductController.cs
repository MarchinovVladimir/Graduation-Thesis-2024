using AIO.Services.Data.Interfaces;
using AIO.Services.Data.Models.Product;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductCategoryService productCategoryService;
		private readonly IAgentService agentService;
		private readonly IProductService productService;
		private readonly IUserService userService;

		public ProductController(IProductCategoryService productCategoryService, IAgentService agentService, IProductService productService, IUserService userService)
		{
			this.productCategoryService = productCategoryService;
			this.agentService = agentService;
			this.productService = productService;
			this.userService = userService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllProductsQueryModel queryModel)
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

			try
			{
				ProductFormModel model = new ProductFormModel()
				{
					Categories = await productCategoryService.GetAllProductCategoriesAsync()
				};
				return View(model);
			}
			catch (Exception)
			{
                return GeneralError();
            }
        }

		[HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
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

			string productId;
			try
			{
				string agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId());
				productId = await productService.CreateProductAndRerurnIdAsync(model, agentId);

				TempData[SuccessMessage] = "Product was successfully added.";
				return RedirectToAction("Details", "Product", new { id = productId });
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while adding the product.");

				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			if (User.IsInRole(AdminRoleName))
			{
				return RedirectToAction("Mine", "Product", new { area = AdminAreaName });
			}

			IEnumerable<ProductAllViewModel> products;

			string userId = this.User.GetId();

			bool isUserAgent = await agentService
				.IsAgentExistByUserIdAsync(userId);
			try
			{
				if (isUserAgent)
				{
					string agentId =
						await this.agentService.GetAgentIdByUserIdAsync(userId);

					products = await productService.GetAllProductsByAgentIdAsync(agentId);
				}
				else
				{
					products = await productService.GetAllProductsByUserIdAsync(userId);
				}

				return View(products);
			}
			catch (Exception)
			{
				return GeneralError();
			}
			
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			try
			{
				ProductDetailsViewModel model = await this.productService
				.GetProductDetailsByIdAsync(id);

				model.Agent.FullName = await productService.GetSellerFullNameByProductIdAsync(id);

				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
			
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			bool isUserAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());
			if (!isUserAgent && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				ProductFormModel formModel = await productService.GetProductFormByIdAsync(id);
				formModel.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(formModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}			
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, ProductFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}

			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			bool isUserAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());
			if (!isUserAgent && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.EditProductByIdAndFormModel(id, model);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occured while tring to edit the product. Please try again later or contact administrator!");
				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();

				return View(model);
			}

			TempData[SuccessMessage] = "Product was successfully edited.";
			return RedirectToAction("Details", "Product", new { id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			bool isUserAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());
			if (!isUserAgent && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				ProductPreDeleteDetailsViewModel viewModel = await productService.GetProductForDeleteByIdAsync(id);
				return View(viewModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id, ProductPreDeleteDetailsViewModel model)
		{
			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			bool isUserAgent = await agentService.IsAgentExistByUserIdAsync(this.User.GetId());
			if (!isUserAgent && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.DeleteProductByIdAsync(id);
				TempData[WarningMessage] = "The product was successfully deleted!";
				return RedirectToAction("Mine", "Product");
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = "Unexpected error occured. Please try again later or contact administrator!";
			return RedirectToAction("Index", "Home");
		}
	}
}
