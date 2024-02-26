using AIO.Web.ViewModels.Agent;

namespace AIO.Services.Data.Interfaces
{
    public interface IAgentService
    {
        Task<bool> IsAgentExistByUserIdAsync(string userId);

        Task<bool> IsAgentExistByPhoneNumberAsync(string phoneNumber);

        Task CreateAsync(string userId, BecomeAgentFormModel model);

        Task<string> GetAgentIdByUserId(string userId);
    }
}
