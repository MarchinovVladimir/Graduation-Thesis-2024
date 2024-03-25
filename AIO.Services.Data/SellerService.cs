using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	/// <summary>
	/// Seller service for working with sellers.
	/// </summary>
	public class SellerService : ISellerService
	{
		private readonly AIODbContext dbContext;

		public SellerService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Service method for checking if seller exist by user id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<bool> IsSellerExistByUserIdAsync(string userId)
		{
			bool result = await this.dbContext
				.Sellers
				.AnyAsync(a => a.UserId.ToString() == userId);

			return result;
		}

		/// <summary>
		/// Service method for checking if seller exist by phone number.
		/// </summary>
		/// <param name="phoneNumber"></param>
		/// <returns></returns>
		public async Task<bool> IsSellerExistByPhoneNumberAsync(string phoneNumber)
		{
			bool result = await this.dbContext
				.Sellers
				.AnyAsync(a => a.PhoneNumber == phoneNumber);

			return result;
		}

		/// <summary>
		/// Service method for creating a new seller.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task CreateAsync(string userId, BecomeSellerFormModel model)
		{
			Seller seller = new Seller
			{
				PhoneNumber = model.PhoneNumber,
				UserId = Guid.Parse(userId)
			};

			await this.dbContext.Sellers.AddAsync(seller);
			await this.dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Service method for getting seller id by user id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<string> GetSellerIdByUserIdAsync(string userId)
		{
			Seller? seller = await this.dbContext.Sellers.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

			if (seller == null)
			{
				return null;
			}

			return seller.Id.ToString();
		}

		/// <summary>
		/// Service method for checking if seller has product with product id.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		public async Task<bool> HasProductWithIdAsync(string userId, string productId)
		{
			Seller? seller = await this.dbContext
				.Sellers
				.Include(a => a.ProductsForSell)
				.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

			if (seller == null)
			{
				return false;
			}

			productId = productId.ToLower();
			return seller.ProductsForSell.Any(p => p.Id.ToString() == productId);
		}
	}
}
