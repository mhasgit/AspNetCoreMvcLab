using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AuthenticationWebApp.Middlewares
{
    public static class UserRoleMapping
    {
        public static Dictionary<string, IEnumerable<string>> UserRoles
             = new Dictionary<string, IEnumerable<string>> {
                 { "Peter", new [] { "User", "Administrator" } },
                 { "Bob", new [] { "User" } },
                 { "Ben", new [] { "User"} }
             };

        public static string[] Users => UserRoles.Keys.ToArray();

        public static Dictionary<string, IEnumerable<Claim>> Claims =>
            UserRoles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(role => new Claim(ClaimTypes.Role, role)), StringComparer.InvariantCultureIgnoreCase);
    }

    public class CustomRoles
    {
        private readonly RequestDelegate _next;

        public CustomRoles(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            IIdentity mainIdentity = context.User.Identity;
            if(mainIdentity.IsAuthenticated && UserRoleMapping.Claims.ContainsKey(mainIdentity.Name))
            {
                ClaimsIdentity rolesIdentity = new ClaimsIdentity("Roles");
                rolesIdentity.AddClaim(new Claim(ClaimTypes.Name, mainIdentity.Name));
                rolesIdentity.AddClaims(UserRoleMapping.Claims[mainIdentity.Name]);
                context.User.AddIdentity(rolesIdentity);
            }
            
            return _next(context);
        }
    }

    public static class CustomRolesExtensions
    {
        public static IApplicationBuilder UseCustomRoles(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomRoles>();
        }
    }
}

