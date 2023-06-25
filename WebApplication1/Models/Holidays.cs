using System.ComponentModel.DataAnnotations;
using HRSystem.Models.Validation;
namespace HRSystem.Models
{
    public class Holidays
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name REQUIRED")]
     //   [Uniquename]
        public String Name { get; set; }

        [Required(ErrorMessage = "Date REQUIRED")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
