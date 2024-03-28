using AIO.Services.Data.Interfaces;
using AIO.Services.Data.Models.Product;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.ErrorMessageConstants.Product;
using static AIOCommon.GeneralAppConstants;
using static AIOCommon.InformationalMessagesConstants.Product;
using static AIOCommon.NotificationMessagesConstants;

namespace AIO.Controllers
{
	/// <summary>
	/// Product controller.
	/// </summary>
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductCategoryService productCategoryService;
		private readonly ISellerService sellerService;
		private readonly IProductService productService;
		private readonly IUserService userService;
		private readonly ILocationAreaService locationAreaService;

		public ProductController(
			IProductCategoryService productCategoryService,
			ISellerService sellerService,
			IProductService productService,
			IUserService userService,
			ILocationAreaService locationAreaService)
		{
			this.productCategoryService = productCategoryService;
			this.sellerService = sellerService;
			this.productService = productService;
			this.userService = userService;
			this.locationAreaService = locationAreaService;
		}

		/// <summary>
		/// All action method. Returns all products.
		/// </summary>
		/// <param name="queryModel"></param>
		/// <returns></returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllProductsQueryModel queryModel)
		{
			await productService.CheckProductIfItIsExpired();

			AllProductsFilteredAndPagedServiceModel serviceModel =
				await productService.GetAllProductsFilteredAndPagedAsync(queryModel);

			queryModel.Products = serviceModel.Products;
			queryModel.TotalProducts = serviceModel.TotalProductsCount;
			queryModel.Categories =
				await productCategoryService.AllProductCategoryNamesAsync();
			queryModel.LocationAreas =
				await locationAreaService.AllLocationAreasNamesAsync();

			return View(queryModel);
		}

		/// <summary>
		/// Add action get method. Returns view for adding a product.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			if (await CheckIfTheUserIsNotSeller())
			{
				return RedirectToAction("Become", "Seller");
			}

			try
			{
				ProductFormModel model = new ProductFormModel();

				model = await LoadsProductFormModelCategoriesAndLocationAreasAsync(model);
				
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Add action post method. Adds a product.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add(ProductFormModel model)
		{
			if (await CheckIfTheUserIsNotSeller())
			{
				return RedirectToAction("Become", "Seller");
			}

			if (!await productCategoryService.ExistsByIdAsync(model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), CategoryNotExistsErrorMessage);
			}

			if (!await locationAreaService.ExistsByIdAsync(model.LocationAreaId))
			{
				ModelState.AddModelError(nameof(model.LocationAreaId), LocationNotExistsErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				model = await LoadsProductFormModelCategoriesAndLocationAreasAsync(model);

				return View(model);
			}

			string productId;

			try
			{
				string sellerId =
					await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
				productId =
					await productService.CreateProductAndRerurnIdAsync(model, sellerId);

				TempData[SuccessMessage] = SuccessfullyAddedProductMessage;

				return RedirectToAction("Details", "Product", new { id = productId });
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, UnsuccesfulProductAddErrorMessage);

				model = await LoadsProductFormModelCategoriesAndLocationAreasAsync(model);

				return View(model);
			}
		}

		/// <summary>
		/// Mine action method. Returns all products of the current user. Sends view model to the view.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			if (User.IsInRole(AdminRoleName))
			{
				return RedirectToAction("Mine", "Product", new { area = AdminAreaName });
			}

			IEnumerable<ProductAllViewModel> products;

			string userId = this.User.GetId();
			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(userId);

			try
			{
				await productService.CheckProductIfItIsExpired();

				if (isUserSeller)
				{
					string sellerId =
						await this.sellerService.GetSellerIdByUserIdAsync(userId);

					products = await productService.GetAllProductsBySellerIdAsync(sellerId);
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

		/// <summary>
		/// Reactivate action method. Reactivates a product with expired date.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Reactivate(string id)
		{
			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = MustBeSellerToReactivateErrorMessage;

				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());

			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = MustBeSellerToReactivateErrorMessage;

				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();
				await productService.ReactivateProductByIdAsync(id);

				return RedirectToAction("Mine", "Product");
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Details action method. Returns details of a product.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();

				ProductDetailsViewModel model =
					await this.productService.GetProductDetailsByIdAsync(id);

				model.Seller.FullName = await productService.GetSellerFullNameByProductIdAsync(id);

				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}

		}

		/// <summary>
		/// Edit action get method. Returns view for editing a product.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();

				ProductFormModel formModel = await productService.GetProductForEditByIdAsync(id);

				formModel = await LoadsProductFormModelCategoriesAndLocationAreasAsync(formModel);

				return View(formModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Edit action post method. Edits a product.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(string id, ProductFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model = await LoadsProductFormModelCategoriesAndLocationAreasAsync(model);

				return View(model);
			}

			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());

			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Mine", "Product");
			}

			await productService.CheckProductIfItIsExpired();

			try
			{
				await productService.EditProductByIdAndFormModel(id, model);
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, GeneralErrorMessage);

				model = await LoadsProductFormModelCategoriesAndLocationAreasAsync(model);

				return View(model);
			}

			TempData[SuccessMessage] = SuccessfullyEditedProductMessage;

			return RedirectToAction("Details", "Product", new { id = id });
		}

		/// <summary>
		/// Delete action get method. Returns view for deleting a product.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());

			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();

				ProductPreDeleteDetailsViewModel viewModel =
					await productService.GetProductForDeleteByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Delete action post method. Deletes a product.Set the IsSold property to true.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Delete(string id, ProductPreDeleteDetailsViewModel model)
		{
			if (await CheckIfProductDoesNotExist(id))
			{
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = MustBeSellerToEditErrorMessage;

				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.DeleteProductByIdAsync(id);

				TempData[WarningMessage] = SuccessfullyDeletedProductMessage;

				return RedirectToAction("Mine", "Product");
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = GeneralErrorMessage;

			return RedirectToAction("Index", "Home");
		}

		private async Task<bool> CheckIfTheUserIsNotSeller()
		{
			bool isSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());

			if (!isSeller)
			{
				TempData[ErrorMessage] = BecomeSellerErrorMessage;
				return true;
			}
			return false;
		}

		private async Task<bool> CheckIfProductDoesNotExist(string id)
		{
			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = ProductDoesNotExistErrorMessage;

				return true;
			}
			return false;
		}

		private async Task<ProductFormModel> LoadsProductFormModelCategoriesAndLocationAreasAsync(ProductFormModel model)
		{
			model.Categories = await productCategoryService.GetAllProductCategoriesAsync();
			model.LocationAreas = await locationAreaService.GetAllLocationAreasAsync();

			return model;
		}
	}
}
