using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
