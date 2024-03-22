using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Agent;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	/// <summary>
	/// Agent service for working with agents.
	/// </summary>
	public class AgentService : IAgentService
	{
		private readonly AIODbContext dbContext;

		public AgentService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Service method for checking if agent exist by user id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<bool> IsSellerExistByUserIdAsync(string userId)
		{
			bool result = await this.dbContext
				.Agents
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
				.Agents
				.AnyAsync(a => a.PhoneNumber == phoneNumber);

			return result;
		}

		public async Task CreateAsync(string userId, BecomeSellerFormModel model)
		{
			Agent agent = new Agent
			{
				PhoneNumber = model.PhoneNumber,
				UserId = Guid.Parse(userId)
			};

			await this.dbContext.Agents.AddAsync(agent);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<string> GetAgentIdByUserIdAsync(string userId)
		{
			Agent? agent = await this.dbContext.Agents.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

			if (agent == null)
			{
				return null;
			}

			return agent.Id.ToString();
		}

		public async Task<bool> HasProductWithIdAsync(string userId, string productId)
		{
			Agent? agent = await this.dbContext
				.Agents
				.Include(a => a.ProductsForSell)
				.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

			if (agent == null)
			{
				return false;
			}

			productId = productId.ToLower();
			return agent.ProductsForSell.Any(p => p.Id.ToString() == productId);
		}
	}
}
