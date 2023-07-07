 using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace HRSystem.ViewModels
{
    public class AttendanceViewModel
    {
        public int ID { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        public DateTime TimeAttendance { get; set; }

        [DataType(DataType.Time)]
        [Remote("Testtime" ,"Attendance",ErrorMessage =" leave time must after attendance time  " ,AdditionalFields = "TimeAttendance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        public DateTime TimeLeave { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Remote("Testdate", "Attendance", ErrorMessage = " date must be after 1/1/2008")]
        public DateTime Date { get; set; }

        public string Employee { get; set; }
        public string Department { get; set; }



    }
}
