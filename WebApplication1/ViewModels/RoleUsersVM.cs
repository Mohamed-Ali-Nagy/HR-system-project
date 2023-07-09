using HRSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRSystem.ViewModels
{
    public class RoleUsersVM
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        //  public List<ApplicationUser> Users { get; set; }=new List<ApplicationUser>();
        //public List<Claim> RoleClaims { get;set; }=new List<Claim>();
        public List<string>? Users { get; set; }=new List<string>();
        public List<string> RoleClaims { get;set; }=new List<string>();



    }
}
