using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Seller;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.ErrorMessageConstants.Seller;
using static AIOCommon.NotificationMessagesConstants;
using static AIOCommon.InformationalMessagesConstants.Seller;

namespace AIO.Controllers
{
	/// <summary>
	/// Seller controller for working with sellers.
	/// </summary>
	[Authorize]
	public class SellerController : Controller
	{
		private readonly ISellerService sellerService;

		public SellerController(ISellerService sellerService)
		{
			this.sellerService = sellerService;
		}

		/// <summary>
		/// Get method for becoming a seller.Checks if user is already a seller.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Become()
		{
			string userId = this.User.GetId();
			bool isSellerExist = await this.sellerService.IsSellerExistByUserIdAsync(userId);
			if (isSellerExist)
			{
				TempData[ErrorMessage] = AlreadyASellerErrorMessage;

				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		/// <summary>
		/// Post method for becoming a seller.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Become(BecomeSellerFormModel model)
		{
			HtmlSanitizer sanitizer = new HtmlSanitizer();

			model.PhoneNumber = sanitizer.Sanitize(model.PhoneNumber);

			string userId = this.User.GetId();
			bool isSellerExist = await this.sellerService.IsSellerExistByUserIdAsync(userId);
			if (isSellerExist)
			{
				TempData[ErrorMessage] = AlreadyASellerErrorMessage;

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await this.sellerService.IsSellerExistByPhoneNumberAsync(model.PhoneNumber);

			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), PhoneNumberTakenErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await sellerService.CreateAsync(userId, model);
				TempData[SuccessMessage] = BecomingSellerSuccessMessage;

			}
			catch (Exception)
			{

				TempData[ErrorMessage] = UnexpectedErrorWhileRegistratingErrorMessage;
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("All", "Product");
		}
	}
}
