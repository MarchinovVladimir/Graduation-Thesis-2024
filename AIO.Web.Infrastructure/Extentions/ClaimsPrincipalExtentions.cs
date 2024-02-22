using System.Security.Claims;

namespace AIO.Web.Infrastructure.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
