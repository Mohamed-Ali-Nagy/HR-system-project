using HRSystem.Constants;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HRSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
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
            List<string> allPermissions=Permission.creatAllPirmissions();
            List<PermissionClaimVM> allClaims = allPermissions.Select(c=>new PermissionClaimVM { ClaimValue=c,isSelected=false}).ToList();
            RolePermisionsVM roleVM = new RolePermisionsVM()
            {
                Pages = Enum.GetNames(typeof(Constants.Models)).ToList(),
                AllPermissions = allClaims,

            };

            return View(roleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task< IActionResult> add(RolePermisionsVM newRole)
        {
            if (!ModelState.IsValid)
            {
                return View(newRole);

            }
            if (await roleManager.FindByNameAsync(newRole.Name)!=null)
            {
                ModelState.AddModelError("", "Role name is already exist");
                return View(newRole);

            }


            bool permissionAdded =newRole.AllPermissions.Any(p=>p.isSelected==true);
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
            else
            {
                ModelState.AddModelError("", "You have to select the permission for the new role group");
                return View(newRole);

            }


        }
        [HttpGet]
        public async Task<IActionResult> edit(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            
            var claimList = await roleManager.GetClaimsAsync(role);
            List<string> allClaims = Permission.creatAllPirmissions();
            List<PermissionClaimVM> allPermissions=allClaims.Select(c=>new PermissionClaimVM { ClaimValue=c}).ToList();

            foreach (var permission in allPermissions)
            {
                if(claimList.Any(c=>c.Value == permission.ClaimValue))
                {
                    permission.isSelected = true;
                }
            }

            RolePermisionsVM roleVM = new RolePermisionsVM()
            {
                Id=role.Id,
                Name = role.Name,
                Pages = Enum.GetNames(typeof(Constants.Models)).ToList(),
                AllPermissions=allPermissions,

            };
            return View(roleVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(RolePermisionsVM roleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roleVM);
            }

            IdentityRole role= await roleManager.FindByIdAsync(roleVM.Id);
            IdentityRole roleByName=await roleManager.FindByNameAsync(roleVM.Name);
            if (roleByName != null && roleByName.Id!=role.Id)
            {
                ModelState.AddModelError("Name", "The name is already existed");
                return View(roleVM);
            }
            else
            {
                role.Name = roleVM.Name;
            }

            bool permissionAdded = roleVM.AllPermissions.Any(p => p.isSelected == true);
            if(permissionAdded)
            {
               var roleClaims=await roleManager.GetClaimsAsync(role);
                foreach (var claim in roleClaims)
                {
                   await roleManager.RemoveClaimAsync(role,claim);
                }
                var newClaims=roleVM.AllPermissions.Where(p => p.isSelected).ToList();
                foreach(var claim in newClaims)
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", claim.ClaimValue));
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You have to select any permission");
                return View(roleVM);        
            }

        }
        public async Task<IActionResult> delete(string id)
        {
            var role=await roleManager.FindByIdAsync(id);
            var users =await userManager.GetUsersInRoleAsync(role.Name);
            if (users != null)
            {
                foreach (var user in users)
                {
                   await userManager.RemoveFromRoleAsync(user, role.Name);
                   await userManager.DeleteAsync(user);

                }

            }
            
            await roleManager.DeleteAsync(role);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Index()
        {
            List<RoleUsersVM> rolesVM = new List<RoleUsersVM>();

            List<IdentityRole> roles = roleManager.Roles.ToList();
            for (int i = 0; i < roles.Count; i++)
            {
                RoleUsersVM roleUsersVM = new RoleUsersVM()
                {
                    Id = roles[i].Id,
                    RoleName= roles[i].Name,
                    Users = (await userManager.GetUsersInRoleAsync(roles[i].Name)).Select(u=>u.UserName).ToList(),
                    RoleClaims = (await roleManager.GetClaimsAsync(roles[i])).Select(c => c.Value).ToList(),
                };
              
                rolesVM.Add(roleUsersVM);
            }

          

            return View(rolesVM);

        }

    }
}
