using System.ComponentModel.DataAnnotations;
using HRSystem.Models.Validation;
namespace HRSystem.Models
{
    public class Holidays
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name REQUIRED")]
        [MinLength(3, ErrorMessage = "Namde must at least 3 characters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Date REQUIRED")]
        [DataType(DataType.Date)]
        [Uniquename(ErrorMessage = "Holiday date is Exist")]
        public DateTime Date { get; set; }
    }

    
}
