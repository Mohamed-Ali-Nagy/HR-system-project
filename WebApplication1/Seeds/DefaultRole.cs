using Microsoft.AspNetCore.Identity;
using HRSystem.Constants;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace HRSystem.Seeds
{
    public static class DefaultRole
    {
        
       public static async Task seedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                //await roleManager.seedClaimsForDefaultRole();
            }
        }

        //public static async Task seedClaimsForDefaultRole(this RoleManager<IdentityRole> roleManager)
        //{
        //    IdentityRole superAdmin = await roleManager.FindByNameAsync("SuperAdmin");
        //    var modelList = Enum.GetValues(typeof(HRSystem.Constants.Models)).OfType<HRSystem.Constants.Models>().ToList();
        //    await roleManager.addPermissionClaim(superAdmin, modelList);
        //}

        //public static async Task addPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole superAdmin, List<HRSystem.Constants.Models> models)
        //{
        //    var allClaims = await roleManager.GetClaimsAsync(superAdmin);

        //    foreach (var model in models)
        //    {
        //        List<string> modelPermissions = Permission.createPermissionsListForModel(model.ToString());
        //        foreach (var permission in modelPermissions)
        //        {
        //            if (!allClaims.Any(c => c.Type == "Permistion" && c.Value == permission))
        //            {
        //                roleManager.AddClaimAsync(superAdmin, new Claim("Permistion", permission));
        //            }
        //        }

        //    }
        //}



    }
}
