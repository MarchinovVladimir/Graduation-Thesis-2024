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

		public ProductController(
			IProductCategoryService productCategoryService,
			ISellerService sellerService,
			IProductService productService,
			IUserService userService)
		{
			this.productCategoryService = productCategoryService;
			this.sellerService = sellerService;
			this.productService = productService;
			this.userService = userService;
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

			if (!ModelState.IsValid)
			{
				model.Categories =
					await productCategoryService.GetAllProductCategoriesAsync();
				return View(model);
			}

			string productId;
			try
			{
				string sellerId =
					await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
				productId =
					await productService.CreateProductAndRerurnIdAsync(model, sellerId);

				TempData[SuccessMessage] = SuccessfullyAddedProduct;

				return RedirectToAction("Details", "Product", new { id = productId });
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, UnsuccesfulProductAddErrorMessage);
				model.Categories = await productCategoryService.GetAllProductCategoriesAsync();

				return View(model);
			}
		}

		/// <summary>
		/// Mine action method. Returns all products of the current user.
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

			bool isUserSeller = 
				await sellerService.IsSellerExistByUserIdAsync(userId);
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

		[HttpGet]
		public async Task<IActionResult> Reactivate(string id)
		{

			bool doesProductExist = await productService.ExistsByIdAsync(id);

			if (!doesProductExist)
			{
				this.TempData[ErrorMessage] = "Product does not exist!";
				return RedirectToAction("All", "Product");
			}

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());
			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a seller to be able to reactivate the product!";
				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to reactivate the product";
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
				await productService.CheckProductIfItIsExpired();

				ProductDetailsViewModel model = await this.productService
				.GetProductDetailsByIdAsync(id);

				model.Seller.FullName = await productService.GetSellerFullNameByProductIdAsync(id);

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

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());
			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become an seller to be able to edit!";
				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();

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

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());
			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a seller to be able to edit!";
				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			await productService.CheckProductIfItIsExpired();

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

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());
			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a seller to be able to edit!";
				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be the product owner to edit the product";
				return RedirectToAction("Mine", "Product");
			}

			try
			{
				await productService.CheckProductIfItIsExpired();

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

			bool isUserSeller = await sellerService.IsSellerExistByUserIdAsync(this.User.GetId());
			if (!isUserSeller && !User.IsAdmin())
			{
				this.TempData[ErrorMessage] = "You must become a seller to be able to edit!";
				return RedirectToAction("Become", "Seller");
			}

			string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId());
			bool isSellerOwner = await productService.IsSellerOwnerOfProductWithIdAsync(id, sellerId);

			if (!isSellerOwner && !User.IsAdmin())
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
	}
}
