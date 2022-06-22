using System.Security.Claims;
using Projekt.Models;
namespace Projekt.Infrastructure
{
    public class help
    {

        public static bool ValidateAdmin(HttpContext httpContext)
        {
        var identity = (ClaimsIdentity)httpContext.User.Identity!;
        var claim = identity.FindFirst(ClaimTypes.Role);

            return claim?.Value == "Admin";
        }
        public static bool ValidateManager(HttpContext httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity!;
            var claim = identity.FindFirst(ClaimTypes.Role);

            return claim?.Value == "Admin" || claim?.Value=="Manager";
        }
        public static bool ValidateWorker(HttpContext httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity!;
            var claim = identity.FindFirst(ClaimTypes.Role);

            return claim?.Value == "Worker" || claim?.Value == "Manager" || claim?.Value=="Admin";
        }
    }
}
