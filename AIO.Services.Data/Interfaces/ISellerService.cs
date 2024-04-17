using AIO.Web.ViewModels.Seller;

namespace AIO.Services.Data.Interfaces
{
    /// <summary>
    /// Interface for SellerService
    /// </summary>
    public interface ISellerService
    {
        /// <summary>
        /// Check if seller exist by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsSellerExistByUserIdAsync(string userId);

        /// <summary>
        /// Check if seller exist by phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<bool> IsSellerExistByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Create new seller
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateAsync(string userId, BecomeSellerFormModel model);

        /// <summary>
        /// Get seller id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetSellerIdByUserIdAsync(string userId);

        /// <summary>
        /// Check if seller has product with id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<bool> HasProductWithIdAsync(string userId, string productId);
    }
}
