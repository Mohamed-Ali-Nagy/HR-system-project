using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Gender EmpGender { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }

        public string NationalId { get; set; }
        public DateTime HireDate { get; set; }
        public double Salary { get; set; }
        public TimeOnly AttendanceTime { get; set; }
        public TimeOnly LeavingTime { get; set; }

        public virtual List<Attendance>? Attendances { get; set; } = new List<Attendance>();

        [ForeignKey("department")]
        public int DeptID { get; set; }
        public virtual Department? department { get; set; }

    }
     public enum Gender
    {
        Male,
        Female
    }
}
