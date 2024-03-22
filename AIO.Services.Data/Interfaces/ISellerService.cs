using AIO.Web.ViewModels.Seller;

namespace AIO.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> IsSellerExistByUserIdAsync(string userId);

        Task<bool> IsSellerExistByPhoneNumberAsync(string phoneNumber);

        Task CreateAsync(string userId, BecomeSellerFormModel model);

        Task<string> GetSellerIdByUserIdAsync(string userId);

        Task<bool> HasProductWithIdAsync(string userId, string productId);
    }
}
