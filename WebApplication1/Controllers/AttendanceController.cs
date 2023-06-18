using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
