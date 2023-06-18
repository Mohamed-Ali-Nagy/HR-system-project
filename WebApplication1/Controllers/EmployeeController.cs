using HRSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepositry employeeRepo;
        public EmployeeController(IEmployeeRepositry employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
