using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.ErrorMessageConstants.Category;
using static AIOCommon.GeneralAppConstants;
using static AIOCommon.NotificationMessagesConstants;


namespace AIO.Areas.Admin.Controllers
{
	public class ProductCategoryController : BaseAdminController
	{
		private readonly IProductCategoryService productCategoryService;

		public ProductCategoryController(IProductCategoryService productCategoryService)
		{
			this.productCategoryService = productCategoryService;
		}

		/// <summary>
		/// Controller action that returns all product categories.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> All()
		{
			try
			{
				ICollection<ProductCategoryViewModel> productCategories = await productCategoryService.GetAllProductCategoriesAsync();

				return View(productCategories);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Controller action that returns the add product category view.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Add()
		{
			ProductCategoryFormModel productCategory = new ProductCategoryFormModel();
			return View(productCategory);
		}

		/// <summary>
		/// Controller action that adds a product category.
		/// </summary>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add(ProductCategoryFormModel productCategory)
		{
			if (await productCategoryService.ExistsByNameAsync(productCategory.Name))
			{
				ModelState.AddModelError(nameof(productCategory.Name), CategoryExistsErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				return View(productCategory);
			}

			await productCategoryService.AddProductCategoryAsync(productCategory);

			return RedirectToAction(nameof(All));
		}

		/// <summary>
		/// Controller action that returns the edit product category view.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!await productCategoryService.ExistsByIdAsync(id))
			{
				TempData[ErrorMessage] = ProductCategoryNotFoundMessage;

				return RedirectToAction("All", "ProductCategory", new { area = AdminAreaName });
			}

			

			try
			{
				ProductCategoryFormModel productCategory = await productCategoryService.GetProductCategoryByIdAsync(id);

				return View(productCategory);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		/// <summary>
		/// Controller action that edits a product category.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="productCategory"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(int id, ProductCategoryFormModel productCategory)
		{
			if (await productCategoryService.ExistsByNameAsync(productCategory.Name))
			{
				ModelState.AddModelError(nameof(productCategory.Name), ProductCategoryExistsErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				return View(productCategory);
			}

			await productCategoryService.EditProductCategoryAsync(id, productCategory);

			return RedirectToAction(nameof(All));
		}

		/// <summary>
		/// Private method that returns a general error message.
		/// </summary>
		/// <returns></returns>
		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = GeneralErrorMessage;

			return RedirectToAction("Index", "Home", new { area = AdminAreaName });
		}
	}
}
