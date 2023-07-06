using Microsoft.AspNetCore.Identity;

namespace HRSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
    }
}
