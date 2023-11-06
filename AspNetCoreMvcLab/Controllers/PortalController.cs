using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcLab.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
