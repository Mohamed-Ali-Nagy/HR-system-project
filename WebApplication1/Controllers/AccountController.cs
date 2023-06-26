using HRSystem.Models;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HRSystem.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> register(Registreviewmodel newuser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FullName = newuser.FullName;
                userModel.UserName = newuser.UserName;
                userModel.Email = newuser.Email;
                userModel.PasswordHash = newuser.Password;

                IdentityResult result = await userManager.CreateAsync(userModel, newuser.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }
            }
            return View(newuser);
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> login(loginviewmodel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await userManager.FindByNameAsync(user.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, user.Password);
                    if (found == true)
                    {

                        await signInManager.SignInAsync(userModel, user.rememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User name or PAssword Wrong");
            }
            return View(user);
        }

        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(Registreviewmodel newuser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newuser.UserName;
                userModel.Email = newuser.Email;
                userModel.FullName = newuser.FullName;
                userModel.PasswordHash = newuser.Password;

                IdentityResult result = await userManager.CreateAsync(userModel, newuser.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("", errorItem.Description);
                    }
                }
            }
            return View(newuser);
        }
    }
}
