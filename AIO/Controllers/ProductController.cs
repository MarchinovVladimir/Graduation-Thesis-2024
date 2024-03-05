﻿using AIO.Services.Data.Interfaces;
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

			try
			{
				string agentId = await agentService.GetAgentIdByUserId(this.User.GetId());
				await productService.CreateProductAsync(model, agentId);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while adding the product.");

				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			IEnumerable<ProductAllViewModel> products;

			string userId = this.User.GetId();

			bool isUserAgent = await agentService
				.IsAgentExistByUserIdAsync(userId);

			if (isUserAgent)
			{
				string agentId =
					await this.agentService.GetAgentIdByUserId(userId);

				products = await productService.GetAllProductsByAgentIdAsync(agentId);
			}
			else
			{
				products = await productService.GetAllProductsByUserIdAsync(userId);
			}

			return View(products);
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
			if (!isUserAgent)
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserId(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner)
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
			if (!isUserAgent)
			{
				this.TempData[ErrorMessage] = "You must become an agent to be able to edit!";
				return RedirectToAction("Become", "Agent");
			}

			string agentId = await agentService.GetAgentIdByUserId(this.User.GetId());
			bool isAgentOwner = await productService.IsAgentOwnerOfProductWithIdAsync(id, agentId);

			if (!isAgentOwner)
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

			return RedirectToAction("Details", "Product", new { id = id });
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = "Unexpected error occured. Please try again later or contact administrator!";
			return RedirectToAction("Index", "Home");
		}
	}
}
