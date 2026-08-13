using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
