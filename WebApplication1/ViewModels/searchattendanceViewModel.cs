using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace HRSystem.ViewModels
{
    public class searchattendanceViewModel
    { 
        public string Name { get; set; }
        public DateTime fromdate { get; set; }
        [Remote("Testdate","Attendance", ErrorMessage ="end date must be more than firstdate" ,AdditionalFields ="fromdate")]
        public DateTime todate { get; set; } = DateTime.Now;
         
    }
}
