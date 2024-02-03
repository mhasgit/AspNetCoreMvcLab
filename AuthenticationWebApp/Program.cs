using AuthenticationWebApp.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthenticationWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthHandler>("QueryAuth", "QueryAuth");
                options.DefaultScheme = "QueryAuth";
            });
            builder.Services.AddAuthorization();

            builder.Services.AddMvc();

            var app = builder.Build();


            //app.UseCustomAuthentication();
            app.UseAuthentication();
            app.UseCustomRoles();
            app.UseRouting();

            app.UseClaimsReporter();

            //app.UseCustomAuthorization();
            app.UseAuthorization();

            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "Everyone can see this page!!!");
                endpoints.MapGet("/admin", AdminEndpoingts.Admin).WithDisplayName("admin");
                endpoints.MapGet("/signin", CustomSignInSignOut.SignIn);
                endpoints.MapGet("/signout", CustomSignInSignOut.SignOut);

                endpoints.MapDefaultControllerRoute();

            });

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }

    public class AdminEndpoingts
    {
        [Authorize(Roles = "Administrator")]
        public static async Task Admin(HttpContext context)
        {
            await context.Response.WriteAsync("This is only accessible by administrators!");
        }
    }


    public class CustomSignInSignOut
    {
        public static async Task SignIn(HttpContext context)
        {
            string user = context.Request.Query["user"];
            if (user != null)
            {
                Claim claim = new Claim(ClaimTypes.Name, user);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("QueryAuth");

                claimsIdentity.AddClaim(claim);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                //context.Response.Cookies.Append("AuthUser", user);
                await context.SignInAsync(principal);
                await context.Response.WriteAsync($"User Authenticated: {user}");
            }
            else
            {
                //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.ChallengeAsync();
            }
        }

        public static async Task SignOut(HttpContext context)
        {
            //context.Response.Cookies.Delete("AuthUser");
            await context.SignOutAsync();
            await context.Response.WriteAsync("User Signed out");
        }
    }


    public class CustomAuthHandler : IAuthenticationSignInHandler
    {
        private HttpContext context;
        private AuthenticationScheme scheme;

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            this.scheme = scheme;
            this.context = context;
            return Task.CompletedTask;
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            AuthenticateResult result;
            string user = this.context.Request.Cookies["AuthUser"];

            if (user != null)
            {
                Claim claim = new Claim(ClaimTypes.Name, user);
                // "QueryAuth"
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.scheme.Name);

                claimsIdentity.AddClaim(claim);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                // context.User = principal;

                var authTicket = new AuthenticationTicket(principal, this.scheme.Name);
                result = AuthenticateResult.Success(authTicket);
            }
            else
            {
                result = AuthenticateResult.NoResult();
            }

            return Task.FromResult(result);
        }

        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            context.Response.Cookies.Append("AuthUser", user.Identity.Name);
            return Task.CompletedTask;
        }

        public Task SignOutAsync(AuthenticationProperties? properties)
        {
            context.Response.Cookies.Delete("AuthUser");
            return Task.CompletedTask;
        }
    }
}