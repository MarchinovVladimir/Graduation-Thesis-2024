using AIO.Web.ViewModels.Agent;

namespace AIO.Services.Data.Interfaces
{
    public interface IAgentService
    {
        Task<bool> IsSellerExistByUserIdAsync(string userId);

        Task<bool> IsSellerExistByPhoneNumberAsync(string phoneNumber);

        Task CreateAsync(string userId, BecomeSellerFormModel model);

        Task<string> GetAgentIdByUserIdAsync(string userId);

        Task<bool> HasProductWithIdAsync(string userId, string productId);
    }
}
