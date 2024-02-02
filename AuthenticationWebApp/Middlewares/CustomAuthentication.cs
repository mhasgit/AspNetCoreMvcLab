using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationWebApp.Middlewares
{
    public class CustomAuthentication
    {
        private readonly RequestDelegate _next;

        public CustomAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string user = context.Request.Cookies["AuthUser"];

            if (user != null)
            {
                Claim claim = new Claim(ClaimTypes.Name, user);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("QueryAuth");

                claimsIdentity.AddClaim(claim);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                context.User = principal;
            }

            return _next(context);
        }
    }

    public static class CustomAuthenticationExtensions
    {
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentication>();
        }
    }
}
