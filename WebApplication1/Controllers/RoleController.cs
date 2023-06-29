using HRSystem.Constants;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HRSystem.Models;
namespace HRSystem.Controllers
{
    public class RoleController : Controller

    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole>_roleManager,UserManager<ApplicationUser>_userManger)
        {
            roleManager = _roleManager;
            userManager= _userManger;
        }
        [HttpGet]
        public IActionResult add()
        {
            RolePermisionsVM roleVM = new RolePermisionsVM();
            roleVM.Pages = Enum.GetNames(typeof(Constants.Models)).ToList(); 
            List<string> allPermissions=Permission.creatAllPirmissions();
            for(int i = 0;i<allPermissions.Count;i++)
            {
                PermissionClaimVM permissionClaimVM = new PermissionClaimVM();
                permissionClaimVM.ClaimValue = allPermissions[i];
                permissionClaimVM.isSelected = false;
             
                roleVM.AllPermissions.Add(permissionClaimVM);
               
            }
 
            return View(roleVM);
        }
        [HttpPost]
        public async Task< IActionResult> add(RolePermisionsVM newRole)
        {
            if(ModelState.IsValid)
            {
               // var all=newRole.AllPermissions;
                if(await roleManager.FindByNameAsync(newRole.Name)==null)
                {
                    bool permissionAdded=newRole.AllPermissions.Any(p=>p.isSelected==true);
                    if(permissionAdded)
                    {
                        IdentityRole role=new IdentityRole();
                        role.Name=newRole.Name;

                        await roleManager.CreateAsync(role);
                        foreach(var permission in newRole.AllPermissions)
                        {
                            if (permission.isSelected == true)
                            {
                                await roleManager.AddClaimAsync(role,new Claim("Permission",permission.ClaimValue));
                            }

                        }
                        return RedirectToAction("Index");

                    }

                    ModelState.AddModelError("", "You have to select the permission for the new role group");
                }
                else
                {
                    ModelState.AddModelError("", "Role name is already exist");

                }
               // newRole.AllPermissions = all;

            }

            return View(newRole);

        }
        public async Task<IActionResult> Index()
        {
            List<RoleUsersVM> rolesVM = new List<RoleUsersVM>();

            List<IdentityRole> roles = roleManager.Roles.ToList();
            for(int i=0; i < roles.Count;i++)
            {
                RoleUsersVM roleUsersVM= new RoleUsersVM();
                roleUsersVM.RoleName = roles[i].Name;
                roleUsersVM.Users = (List<ApplicationUser>)await userManager.GetUsersInRoleAsync(roles[i].Name);
                roleUsersVM.RoleClaims = (List<Claim>)await roleManager.GetClaimsAsync(roles[i]);
                rolesVM.Add(roleUsersVM);
            }

         
            return View(rolesVM);

        }

    }
}
