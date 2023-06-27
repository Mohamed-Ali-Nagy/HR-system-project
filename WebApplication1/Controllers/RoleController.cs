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
                                return RedirectToAction("Index");
                            }
                        }
                    }

                    ModelState.AddModelError("", "You have to select the permission for the new role group");
                }

                ModelState.AddModelError("", "Role name is already exist");
            }
            return View(newRole);

        }
        public async Task<IActionResult> Index()
        {
           // List<RoleUsersVM> rolesVM = new List<RoleUsersVM>();

            List<IdentityRole> roles = roleManager.Roles.ToList();
            foreach(var role in roles)
            {
                ViewData[$"{role.Name}Users"] =await userManager.GetUsersInRoleAsync(role.Name.ToString());
                ViewData[$"{role.Name}Claims"] = await roleManager.GetClaimsAsync(role);
            }          
            //List<ApplicationUser> usersForEachModel = new List<ApplicationUser>();
            //for (int i = 0; i < roles.Count; i++)
            //{
            //    rolesVM[i].RoleName = roles[i].Name;
            //    //if (roles[i].Name!=null)
            //    //{
            //    usersForEachModel = (List<ApplicationUser>)await userManager.GetUsersInRoleAsync(roles[i].Name);

            //    //}
            //    for (int j = 0; j < usersForEachModel.Count; j++)
            //    {
            //        rolesVM[i].UserEmail[j] = usersForEachModel[j].Email;
            //    }
            //}
            return View(roles);

        }
    }
}
