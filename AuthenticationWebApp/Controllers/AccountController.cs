using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                Claim nameClaim = new Claim(ClaimTypes.Name, userName);
                ClaimsIdentity ident = new ClaimsIdentity("QueryAuth");
                ident.AddClaim(nameClaim);

                this.HttpContext.SignInAsync(new ClaimsPrincipal(ident));

                return View();
            }
            else
            {
                return View();
            }
        }

        public IActionResult SignOut()
        {
            this.HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
