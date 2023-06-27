using HRSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.ViewModels
{
    public class SalaryReportViewModel
    {
        public int Id { get; set; }
        //[Display(Name = "Employee Name")]
        public string EmpNme { set; get;}
        //[Display(Name = "Department Name")]
        public string? DeptName { set; get;}
        //[Display(Name = "Basic Salary")]
        public double BasicSalary { set; get;}
        public Month Month { set; get;}
        public int? Attendancedays { set; get;}
        public int? AbsenceDays { set; get;}
        public double? AddHours { set; get;}
        public double? DedacatedHours { set; get;}
        public double? TotalAdd { set; get; }
        public double?   TotalDedacated { set; get; }
        public double? TotalDue { set; get; }
    }   
}
