using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;

namespace HRSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public IActionResult Index()
        {
            return Content("Hello Admin");
        }

        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel roleViewMode)
        {
            if (ModelState.IsValid == true)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = roleViewMode.RoleName;
                
                IdentityResult result = await roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(roleViewMode);
        }
    }
}
