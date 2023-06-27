using HRSystem.Models;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class UserController : Controller
    {
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<ApplicationUser>_userManger,RoleManager<IdentityRole>_roleManager)
        {
           userManager=_userManger;
            roleManager=_roleManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult add()
        {
            ApplicationUserGroupVM applicationUserGroupVM = new ApplicationUserGroupVM();
            //applicationUserGroupVM.Groups=
            return View();

        }

    }
}
