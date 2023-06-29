using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.ViewModels
{
    public class ApplicationUserGroupVM
    {
        //public string Id { get; set; }
        [Required(ErrorMessage = "The name is Required")]
        [RegularExpression(@"^[A-Za-z]+(\s[A-Za-z]+)$", ErrorMessage = "The full name must be at least 2 names and contains only char")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get;set; }
        [Required]
        [Display(Name="User Name")]
        [Key]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<IdentityRole> Groups { get; set; } = new List<IdentityRole>();

    }
}
