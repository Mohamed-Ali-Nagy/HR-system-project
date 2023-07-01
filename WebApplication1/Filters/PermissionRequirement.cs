using Microsoft.AspNetCore.Authorization;

namespace HRSystem.Filters
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirement(string _permission)
        {
            Permission = _permission;

        }
    }
}
