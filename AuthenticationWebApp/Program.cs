using AuthenticationWebApp.Middlewares;

namespace AuthenticationWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseCustomAuthentication();
            app.UseCustomRoles();
            app.UseRouting();

            app.UseClaimsReporter();

            app.UseCustomAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "Everyone can see this page!!!");
                endpoints.MapGet("/admin", () => "This is only accessible by administrators!").WithDisplayName("admin");
                endpoints.MapGet("/signin", CustomSignInSignOut.SignIn);
                endpoints.MapGet("/signout", CustomSignInSignOut.SignOut);

            });

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }


    public class CustomSignInSignOut
    {
        public static async Task SignIn(HttpContext context)
        {
            string user = context.Request.Query["user"];
            if (user != null)
            {
                context.Response.Cookies.Append("AuthUser", user);
                await context.Response.WriteAsync($"User Authenticated: {user}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        public static async Task SignOut(HttpContext context)
        {
            context.Response.Cookies.Delete("AuthUser");
            await context.Response.WriteAsync("User Signed out");
        }
    }

}