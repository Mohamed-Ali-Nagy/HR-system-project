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
                                                    Id = u.Id,
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
        public async Task<IActionResult> edit(ApplicationUserGroupVM userVM)
        {
            userVM.Groups = roleManager.Roles.ToList();

            if (!ModelState.IsValid)
            {
                return View(userVM);
            }
            ApplicationUser user = await userManager.FindByIdAsync(userVM.Id);
            if(await userManager.FindByEmailAsync(userVM.Email)!=null&&userVM.Email!=user.Email)
            {
                ModelState.AddModelError("Email", "email already existed");
                return View(userVM);
            }
           
          
            if (await userManager.FindByNameAsync(userVM.UserName) != null && userVM.UserName != user.UserName)
            {
                ModelState.AddModelError("userName", "UserName already existed");
                return View(userVM);
            }
        
            string token =await userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult result= await userManager.ResetPasswordAsync(user, token, userVM.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Password",error.Description);
                }
                return View(userVM);
            }

            user.Name = userVM.Name;
            await userManager.AddToRoleAsync(user, userVM.RoleName);

            return RedirectToAction("Index");
        }
        #endregion






    }
}
