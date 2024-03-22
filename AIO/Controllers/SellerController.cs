using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Seller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants;

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
				TempData[ErrorMessage] = "You are already a seller";

				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeSellerFormModel model)
		{
			string userId = this.User.GetId();
			bool isSellerExist = await this.sellerService.IsSellerExistByUserIdAsync(userId);
			if (isSellerExist)
			{
				TempData[ErrorMessage] = "You are already a seller!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await this.sellerService.IsSellerExistByPhoneNumberAsync(model.PhoneNumber);

			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number is already taken");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await sellerService.CreateAsync(userId, model);
				
			}
			catch (Exception)
			{

				TempData[ErrorMessage] = 
					"Unexpexted error occured while registrating you a seller! Please try again later or contact administrator";
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
