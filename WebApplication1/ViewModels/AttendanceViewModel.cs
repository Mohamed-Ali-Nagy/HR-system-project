 using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace HRSystem.ViewModels
{
    public class AttendanceViewModel
    {
        public int ID { get; set; }
        public DateTime TimeAttendance { get; set; }
        [Remote("Testtime" ,"Attendance",ErrorMessage =" leave time must after attendance time  " ,AdditionalFields = "TimeAttendance")]
        public DateTime TimeLeave { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Remote("Testdate", "Attendance", ErrorMessage = " date must be after 1/1/2008")]
        public DateTime Date { get; set; }

        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }



    }
}
