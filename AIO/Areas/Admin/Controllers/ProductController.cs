using AIO.Areas.Admin.ViewModels;
using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace AIO.Areas.Admin.Controllers
{
	public class ProductController : BaseAdminController
	{
		private readonly IAgentService agentService;
		private readonly IProductService productService;

		public ProductController(IAgentService agentService, IProductService productService)
		{
			this.agentService = agentService;
			this.productService = productService;
		}

		public async Task<IActionResult> Mine()
		{
			string agentId = await this.agentService.GetAgentIdByUserIdAsync(User.GetId());

			MyProductsViewModel viewModel = new MyProductsViewModel()
			{
				AddedProducts = await productService.GetAllProductsByAgentIdAsync(agentId)
			};

			return View(viewModel);
		}
	}
}
