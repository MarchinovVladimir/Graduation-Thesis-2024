using AIO.Data;
using AIO.Data.Models;
using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.Agent;
using Microsoft.EntityFrameworkCore;

namespace AIO.Services.Data
{
	public class AgentService : IAgentService
	{
		private readonly AIODbContext dbContext;

		public AgentService(AIODbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<bool> IsAgentExistByUserIdAsync(string userId)
		{
			bool result = await this.dbContext
				.Agents
				.AnyAsync(a => a.UserId.ToString() == userId);

			return result;
		}

		public async Task<bool> IsAgentExistByPhoneNumberAsync(string phoneNumber)
		{
			bool result = await this.dbContext
				.Agents
				.AnyAsync(a => a.PhoneNumber == phoneNumber);

			return result;
		}

		public async Task CreateAsync(string userId, BecomeAgentFormModel model)
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
