using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        [RegularExpression("@^[A-Z][a-z]+(\\s[A-Z][a-z]+){2,3}$", ErrorMessage ="The full name must be at least 3 names and contains only char")]
        public string Name { get; set; }
        public string Address { get; set; }
        [RegularExpression(@"^01(0|1|2|5)\d{8}$",ErrorMessage = "Phone Must be 11 Numbers Begins with (010,011,012,015)")]
        public string Phone { get; set; }
        
        public Gender EmpGender { get; set; }

        public string Nationality { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
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
