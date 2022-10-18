using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class AlchemyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
