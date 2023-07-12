using HRSystem.Models;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class UserController : Controller
    {
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<ApplicationUser>_userManger,RoleManager<IdentityRole>_roleManager)
        {
           userManager=_userManger;
            roleManager=_roleManager;
            
        }
        #region Index
        public async Task<IActionResult> Index()
        {

            List<ApplicationUserGroupVM> users =  userManager.Users
                                                .Select( u => new ApplicationUserGroupVM 
                                                {
                                                    Id = u.Id,
                                                    Name = u.Name,
                                                    UserName = u.UserName,
                                                    Email = u.Email,
                                              
                                                }).ToList();
          
            return View(users);
        }
        #endregion

        #region Add 
        [HttpGet]
        public IActionResult add()
        {
            ApplicationUserGroupVM applicationUserGroupVM = new ApplicationUserGroupVM();
            applicationUserGroupVM.Groups=roleManager.Roles.ToList();
            return View(applicationUserGroupVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add(ApplicationUserGroupVM newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user1 = await userManager.FindByEmailAsync(newUser.Email);
                if (user1 == null && await userManager.FindByNameAsync(newUser.UserName) == null)
                {
                    ApplicationUser user = new ApplicationUser()
                    {

                        Name = newUser.Name,
                        Email = newUser.Email,
                        UserName = newUser.UserName,

                    };

                    IdentityResult isCreated = await userManager.CreateAsync(user, newUser.Password);
                    if (isCreated.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, newUser.RoleName);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in isCreated.Errors)
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                    }
                }
                else if (user1 != null)
                {
                    ModelState.AddModelError("Email", "Email is already existed");

                }
                else
                {
                    ModelState.AddModelError("userName", "Username is already existed");
                }

            }
            newUser.Groups=roleManager.Roles.ToList();
            return View(newUser);

        }
        #endregion

        #region delete
        public async Task<IActionResult> delete(string id)
        {
           ApplicationUser user= await userManager.FindByIdAsync(id);
            if (user != null)
            {
                string role = userManager.GetRolesAsync(user).Result.FirstOrDefault();
                await userManager.RemoveFromRoleAsync(user, role);
                await userManager.DeleteAsync(user);

            }
           return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> edit(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            ApplicationUserGroupVM userVM = new ApplicationUserGroupVM()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Groups = roleManager.Roles.ToList(),
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> edit(ApplicationUserGroupVM userVM)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(userVM.Id);
                if (await userManager.FindByEmailAsync(userVM.Email) != null && userVM.Email != user.Email)
                {
                    ModelState.AddModelError("Email", "Email already existed");
                    return View(userVM);
                }


                if (await userManager.FindByNameAsync(userVM.UserName) != null && userVM.UserName != user.UserName)
                {
                    ModelState.AddModelError("userName", "UserName already existed");
                    return View(userVM);
                }

                user.Name = userVM.Name;
                user.Email = userVM.Email;
                user.UserName = userVM.UserName;
                await userManager.UpdateAsync(user);
                var oldRole = (await userManager.GetRolesAsync(user)).ToList().FirstOrDefault();
                await userManager.RemoveFromRoleAsync(user, oldRole);

                await userManager.AddToRoleAsync(user, userVM.RoleName);

                return RedirectToAction("Index");
            }

            else
            {
                userVM.Groups = roleManager.Roles.ToList();
                return View(userVM);

            }
        }
        #endregion






    }
}
