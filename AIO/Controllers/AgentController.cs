using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants;

namespace AIO.Controllers
{
	/// <summary>
	/// Seller controller for working with sellers.
	/// </summary>
	[Authorize]
	public class AgentController : Controller
	{
		private readonly IAgentService agentService;

		public AgentController(IAgentService agentService)
		{
			this.agentService = agentService;
		}

		/// <summary>
		/// Get method for becoming a seller.Checks if user is already a seller.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Become()
		{
			string userId = this.User.GetId();
			bool isAgentExist = await this.agentService.IsSellerExistByUserIdAsync(userId);
			if (isAgentExist)
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
			bool isAgentExist = await this.agentService.IsSellerExistByUserIdAsync(userId);
			if (isAgentExist)
			{
				TempData[ErrorMessage] = "You are already a seller!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await this.agentService.IsSellerExistByPhoneNumberAsync(model.PhoneNumber);

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
				await this.agentService.CreateAsync(userId, model);
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
