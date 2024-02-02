using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationWebApp.Middlewares
{
    public class ClaimsReporter
    {
        private readonly RequestDelegate _next;

        public ClaimsReporter(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            ClaimsPrincipal p = context.User;

            Console.WriteLine(new string('*', 15));
            Console.WriteLine($"User: {p.Identity.Name}");
            Console.WriteLine($"Authenticated: {p.Identity.IsAuthenticated}");
            Console.WriteLine($"Authentication Type: {p.Identity.AuthenticationType}");
            Console.WriteLine($"Identities: {p.Identities.Count()}");
            
            foreach(ClaimsIdentity iden in p.Identities)
            {
                Console.WriteLine($"Auth Type: {iden.AuthenticationType}");
                Console.WriteLine($"Claims: {iden.Claims.Count()}");
                
                foreach(Claim claim in iden.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}, Issuer: {claim.Issuer}");
                }

            }

            Console.WriteLine(new string('*', 15));
            Console.WriteLine();
            Console.WriteLine();


            return _next(context);
        }
    }

    public static class ClaimsReporterExtensions
    {
        public static IApplicationBuilder UseClaimsReporter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsReporter>();
        }
    }
}
