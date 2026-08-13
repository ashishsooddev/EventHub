using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class RegistrationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
