using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreMvcLab
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomAuthentication
    {
        private readonly RequestDelegate _next;

        public CustomAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            //string userName = context.Request.Query["user"];
            string userName = context.Request.Cookies["user"];
            if (userName != null)
            {
                var identity = new ClaimsIdentity("QueryTypeAuth");
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                context.User = new ClaimsPrincipal(identity);
            }

            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserQueryAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserQueryAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentication>();
        }
    }
}
