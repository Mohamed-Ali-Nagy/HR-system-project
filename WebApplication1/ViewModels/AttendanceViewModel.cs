using HRSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.ViewModels
{
    public class AttendanceViewModel
    {
        public int ID { get; set; }
        public DateTime TimeAttendance { get; set; }
        public DateTime TimeLeave { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }



    }
}
