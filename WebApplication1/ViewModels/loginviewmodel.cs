using System.ComponentModel.DataAnnotations;

namespace HRSystem.ViewModels
{
    public class loginviewmodel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool rememberMe { get; set; }
    }
}
