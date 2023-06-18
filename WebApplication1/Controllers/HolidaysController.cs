using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class HolidaysController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
