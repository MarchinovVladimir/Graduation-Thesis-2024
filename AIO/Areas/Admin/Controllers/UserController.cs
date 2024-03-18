using AIO.Services.Data.Interfaces;
using AIO.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Areas.Admin.Controllers
{
	public class UserController : BaseAdminController
	{
		private readonly IUserService userService;
		private readonly IMemoryCache memoryCache;

		public UserController(IUserService userService, IMemoryCache memoryCache)
		{
			this.userService = userService;
			this.memoryCache = memoryCache;
		}

		[Route("User/All")]
		public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> users = memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);

			if(users == null)
			{
				users = await userService.AllAsync();
				MemoryCacheEntryOptions cacheOptions = 
					new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromMinutes(UsersCacheDurationInMinutes));

				memoryCache.Set(UsersCacheKey, users, cacheOptions);
			}	

			return View(users);
		}
	}
}
