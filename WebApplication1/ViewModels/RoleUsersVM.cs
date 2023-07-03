using HRSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRSystem.ViewModels
{
    public class RoleUsersVM
    {
        public string RoleName { get; set; }
        public List<ApplicationUser> Users { get; set; }=new List<ApplicationUser>();
        public List<Claim> RoleClaims { get;set; }=new List<Claim>();

    }
}
