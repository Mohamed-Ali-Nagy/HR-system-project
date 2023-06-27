using Microsoft.Build.Framework;

namespace HRSystem.ViewModels
{
    public class RolePermisionsVM
    {
       
        [Required]
        public string Name { get; set; }
        public List<string>? Pages { get; set; }=new List<string>();
        public List<PermissionClaimVM>? AllPermissions { get; set; }=new List<PermissionClaimVM>();
    }
}
