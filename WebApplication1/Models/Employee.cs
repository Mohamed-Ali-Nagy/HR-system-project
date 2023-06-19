using HRSystem.Models.Validation;
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
        [Required]
        [UniquePhone]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$",ErrorMessage = "Phone Must be 11 Numbers Begins with (010,011,012,015)")]
        public string Phone { get; set; }
        
        public Gender EmpGender { get; set; }

        public string Nationality { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        [Range(typeof(DateTime), "1/1/1970", "1/1/2000")]
        public DateTime BirthDate { get; set; }
        [RegularExpression(@"\r\n^([1-9]{1})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})[0-9]{3}([0-9]{1})[0-9]{1}$",ErrorMessage ="Invalid national Id")]
        public string NationalId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        [Range(typeof(DateTime), "1/1/2008", "1/1/2023")]
        public DateTime HireDate { get; set; }
        [Column(TypeName = "Money")]
        [Range(8000,50000,ErrorMessage ="The salary Range (8k:50k)")]
        public double Salary { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString ="{0:hh:mm:ss}")]
        public DateTime AttendanceTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
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
