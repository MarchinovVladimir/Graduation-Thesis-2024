using AIO.Data;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	public class ProductService : IProductService
	{
		private readonly AIODbContext dbContext;

        public ProductService(AIODbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetFirstThreeExpiringProducts()
		{
			IEnumerable<ProductIndexViewModel> firstThreeExpireProducts = await this.dbContext.Products
				.AsNoTracking()
				.Where(p => p.IsAuctionClosed == false)
				.OrderBy(p => p.EndTime)
				.Take(3)
				.Select(p => new ProductIndexViewModel
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					ImageUrl = p.ImageUrl,
				}).ToArrayAsync();

			return firstThreeExpireProducts;
		}
	}
}
