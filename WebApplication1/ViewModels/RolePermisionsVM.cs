using Microsoft.Build.Framework;

namespace HRSystem.ViewModels
{
    public class RolePermisionsVM
    {

        //public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<string>? Pages { get; set; }=new List<string>();
        public List<PermissionClaimVM>? AllPermissions { get; set; }=new List<PermissionClaimVM>();
    }
}
