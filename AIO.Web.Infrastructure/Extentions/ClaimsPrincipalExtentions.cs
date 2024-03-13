using System.Security.Claims;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Web.Infrastructure.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
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
