namespace AuthClaims
{
    using System.ComponentModel.Design;
    using System.Security.Claims;
    using System.Security.Principal;

    internal class Program
    {
        static void Main(string[] args)
        {
            // Aspnet Core Identity

            // Who are you?
            // Authentication: Identifying who the user is


            // Are you allowed to perform this?
            // Authorization: Can the person with the identity perform an action he is requesting

            // Claim based authentication

            Claim nameClaim = new Claim(ClaimTypes.Name, "hasssan");
            Claim emailClaim = new Claim(ClaimTypes.Email, "hasssan.movlo@gmail.com");

            ClaimsIdentity claimsIdentity = new ClaimsIdentity("BasicAuth");
            claimsIdentity.AddClaim(nameClaim);
            claimsIdentity.AddClaim(emailClaim);

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity); // Per user

            // WindowsIdentity currentWindowsUser = WindowsIdentity.GetCurrent();


            var emClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
            if (emClaim != null)
            {
                //
            }
        }

        static void Next()
        {
            var requestPipeline = new[]
            {
                UseStaticFiles,
                UseRouting,
                UseControllerWithViews
            };

            foreach (var middleware in requestPipeline)
            {

            }


        }


        static void UseStaticFiles()
        {
            // Console.WriteLine("Before");

            // context, delegate
            Next();


            Console.WriteLine("After");
        }


        static void UseRouting()
        {
            Console.WriteLine("Before");

            Next();


            Console.WriteLine("After");
        }

        static void UseControllerWithViews()
        {
            Console.WriteLine("Before");

            Next();


            Console.WriteLine("After");
        }
    }
}