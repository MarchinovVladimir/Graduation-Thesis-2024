using AIO.Services.Data.Interfaces;
using AIO.Web.Infrastructure.Extentions;
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
            bool isAgentExist = await this.agentService.IsAgentExistByUserId(userId);
            if (isAgentExist)
            {
				TempData[ErrorMessage] = "You are already an Agent";

				return RedirectToAction("Index", "Home");
            }
            return View();
		}
	}
}
