using HRSystem.Models;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginUserVM loginUser)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user= await userManager.FindByEmailAsync(loginUser.Email);
                if ( user!= null)
                {
                    bool validPassword =await userManager.CheckPasswordAsync(user,loginUser.Password);
                    if (validPassword)
                    {
                        await signInManager.SignInAsync(user, loginUser.RememberMe);
                        RedirectToAction("Index", "Home");

                    }
                    ModelState.AddModelError("Password", "Wrong Password");
                }
                ModelState.AddModelError("Email", "Wrong Email");
                
            }
            return View(loginUser);
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
