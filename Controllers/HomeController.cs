using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
