using System.Security.Claims;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Web.Infrastructure.Extentions
{
    /// <summary>
    /// Extention class for ClaimsPrincipal.
    /// </summary>
    public static class ClaimsPrincipalExtentions
    {
        /// <summary>
        /// Method for getting user id from ClaimsPrincipal.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
			return user.IsInRole(AdminRoleName);
		}
    }
}
