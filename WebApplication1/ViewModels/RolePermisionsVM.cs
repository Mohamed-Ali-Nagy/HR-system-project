using Microsoft.Build.Framework;

namespace HRSystem.ViewModels
{
    public class RolePermisionsVM
    {
       // public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<PermissionClaimVM> AllPermissions { get; set; }
    }
}
