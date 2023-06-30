using HRSystem.Models;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        #region Index
        public async Task<IActionResult> Index()
        {

            List<ApplicationUserGroupVM> users =  userManager.Users
                                                .Select( u => new ApplicationUserGroupVM 
                                                {
                                                    
                                                    Name = u.Name,
                                                    UserName = u.UserName,
                                                    Email = u.Email,
                                                   // Password = u.PasswordHash,
                                                    
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
        public async Task<IActionResult> add(ApplicationUserGroupVM newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            if (await userManager.FindByEmailAsync(newUser.Email) == null&& await userManager.FindByNameAsync(newUser.UserName) == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    //Id= newUser.Id,
                    Name = newUser.Name,
                    Email = newUser.Email,
                    UserName = newUser.UserName,

                };

                IdentityResult isCreated=await userManager.CreateAsync(user,newUser.Password);
                if(isCreated.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, newUser.RoleName);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in isCreated.Errors)
                    {
                       ModelState.AddModelError("Password",error.Description);
                    }
                }
            }
         
            else
            {
                ModelState.AddModelError("", "Email or user name is already exist");
            }
            return View(newUser);

        }
        #endregion

        #region delete
        public async Task<IActionResult> delete(string email)
        {
           ApplicationUser user= await userManager.FindByEmailAsync(email);
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
        //[HttpGet]
        //public async Task< IActionResult> edit(string email)
        //{
        //    ApplicationUser user =await userManager.FindByEmailAsync (email);
        //    ApplicationUserGroupVM groupVM = new ApplicationUserGroupVM();
        //    return View();
        //}
        #endregion






    }
}
