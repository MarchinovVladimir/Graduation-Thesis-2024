using AIO.Data;
using AIO.Services.Data.Interfaces;
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

        public async Task<bool> IsAgentExistByUserId(string userId)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);
            return result;
        }
    }
}
