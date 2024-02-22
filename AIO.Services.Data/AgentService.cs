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
	}
}
