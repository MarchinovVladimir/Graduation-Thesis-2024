using AIO.Areas.Admin.ViewModels;
using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace AIO.Areas.Admin.Controllers
{
	public class ProductController : BaseAdminController
	{
		private readonly ISellerService sellerService;
		private readonly IProductService productService;

		public ProductController(ISellerService sellerService, IProductService productService)
		{
			this.sellerService = sellerService;
			this.productService = productService;
		}

		public async Task<IActionResult> Mine()
		{
			string sellerId = await this.sellerService.GetSellerIdByUserIdAsync(User.GetId());

			MyProductsViewModel viewModel = new MyProductsViewModel()
			{
				AddedProducts = await productService.GetAllProductsBySellerIdAsync(sellerId)
			};

			return View(viewModel);
		}
	}
}
