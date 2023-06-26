using HRSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HRSystem.Seeds
{
    public static class DefaultUser
    {
        public static async Task seedUser(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new ApplicationUser()
            {
                Email = "nagy@admin.com",
                UserName = "nagy@admin",
                PasswordHash = "P@word"
            };
             //ApplicationUser user=await userManager.FindByEmailAsync(defaultUser.Email);
            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(defaultUser, defaultUser.PasswordHash);
                await userManager.AddToRoleAsync(defaultUser,"SuperAdmin");
            }

        }

    }
}
