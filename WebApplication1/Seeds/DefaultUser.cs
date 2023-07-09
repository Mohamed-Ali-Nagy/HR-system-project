using HRSystem.Constants;
using HRSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRSystem.Seeds
{
    public static class DefaultUser
    {
        public static async Task seedUser(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            ApplicationUser defaultUser = new ApplicationUser()
            {
                Email = "mamn1111997@gmail.com",
                UserName = "mamn1111997@gmail.com",
                PasswordHash = "P@ssword1"
            };
            //ApplicationUser user=await userManager.FindByEmailAsync(defaultUser.Email);
            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(defaultUser, "P@ssword1");
                await userManager.AddToRoleAsync(defaultUser,"SuperAdmin");
                await roleManager.seedClaimsForDefaultRole();

            }

        }

        public static async Task seedClaimsForDefaultRole(this RoleManager<IdentityRole> roleManager)
        {
            IdentityRole superAdmin = await roleManager.FindByNameAsync("SuperAdmin");
            List<string> modelList=Enum.GetNames(typeof(Constants.Models)).ToList();
            await roleManager.addPermissionClaim(superAdmin, modelList);
        }

        public static async Task addPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole superAdmin, List<string> models)
        {
            var allClaims = await roleManager.GetClaimsAsync(superAdmin);

            foreach (var model in models)
            {
                List<string> modelPermissions = Permission.createPermissionsListForModel(model);
                foreach (var permission in modelPermissions)
                {
                    if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    {
                       await roleManager.AddClaimAsync(superAdmin, new Claim("Permission", permission));
                    }
                }

            }
        }

    }
}
