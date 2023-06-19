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
        public DateTime AttendanceTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual List<Attendance>? Attendances { get; set; } = new List<Attendance>();

        [ForeignKey("department")]
        public int? DeptID { get; set; }
        public virtual Department? department { get; set; }

    }
     public enum Gender
    {
        Male,
        Female
    }
}
