using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
using AIO.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AIOCommon.NotificationMessagesConstants;

namespace AIO.Controllers
{
	[Authorize]
	public class AgentController : Controller
	{
		private readonly IAgentService agentService;

		public AgentController(IAgentService agentService)
		{
			this.agentService = agentService;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			string userId = this.User.GetId();
			bool isAgentExist = await this.agentService.IsAgentExistByUserIdAsync(userId);
			if (isAgentExist)
			{
				TempData[ErrorMessage] = "You are already an Agent";

				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{
			string userId = this.User.GetId();
			bool isAgentExist = await this.agentService.IsAgentExistByUserIdAsync(userId);
			if (isAgentExist)
			{
				TempData[ErrorMessage] = "You are already an Agent";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await this.agentService.IsAgentExistByPhoneNumberAsync(model.PhoneNumber);

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
					"Unexpexted error occured while registrating you as agent! Please try again later or contact administrator";
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("All", "Home");
		}
	}
}
