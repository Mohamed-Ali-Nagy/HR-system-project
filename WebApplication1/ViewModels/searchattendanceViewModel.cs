using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace HRSystem.ViewModels
{
    public class searchattendanceViewModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime fromdate { get; set; }
        [Remote("Testenddate","Attendance", ErrorMessage ="end date must be more than firstdate" ,AdditionalFields ="fromdate")]
        public DateTime todate { get; set; } 
         
    }
}
