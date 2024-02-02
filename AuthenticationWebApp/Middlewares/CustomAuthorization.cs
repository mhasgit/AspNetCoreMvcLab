using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AuthenticationWebApp.Middlewares
{
    public class CustomAuthorization
    {
        private readonly RequestDelegate _next;

        public CustomAuthorization(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.GetEndpoint()?.DisplayName == "admin")
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if(context.User.IsInRole("Administrator"))
                    {
                        await _next(context);
                    }
                    else
                    {
                        Forbid(context);
                    }
                } 
                else
                {
                    Challenge(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        public void Forbid(HttpContext context) =>
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

        public void Challenge(HttpContext context) =>
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    public static class CutomAutherizationeExtensions
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorization>();
        }
    }
}
