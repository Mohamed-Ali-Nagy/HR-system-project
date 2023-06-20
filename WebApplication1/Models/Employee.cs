using HRSystem.Models.Validation;
using HRSystem.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The name is Required")]
        [RegularExpression(@"^[A-Za-z]+(\s[A-Za-z]+)$", ErrorMessage ="The full name must be at least 2 names and contains only char")]
        public string Name { get; set; }


        [Required(ErrorMessage ="You must enter your Address")]
        public string Address { get; set; }


        [Required(ErrorMessage ="Phone number is required")]
        //[UniquePhone]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$",ErrorMessage = "Phone Must be 11 Numbers Begins with (010,011,012,015)")]
        public string Phone { get; set; }
        
        public Gender EmpGender { get; set; }


        [Required(ErrorMessage ="The Nationality is required")]
        public string Nationality { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="0:yyyy/mm/dd")]
        [Display(Name = "Birth date")]
        [Required(ErrorMessage ="The birth date is required")]
        [BirthDateValidation]
        public DateTime BirthDate { get; set; }


        [RegularExpression(@"^(2|3|4|6|8)[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])[0-9]{7}$", ErrorMessage ="Invalid national Id")]
        [Required(ErrorMessage ="The National Id number is required")]
        public string NationalId { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Hire date")]
        [DisplayFormat(DataFormatString = "0:yyyy/mm/dd")]
        [Required(ErrorMessage = "The Hiring date is required")]
        [HireDateValidation]
        public DateTime HireDate { get; set; }


        [Column(TypeName = "Money")]
        [Range(8000,50000,ErrorMessage ="The salary Range (8k:50k)")]
        [Required(ErrorMessage ="The salary is required")]
        public double Salary { get; set; }


        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString ="{0:hh:mm:ss}")]
        [Required(ErrorMessage = "The attendance Time is required")]
        public DateTime AttendanceTime { get; set; }


        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        [Required(ErrorMessage = "The Hiring leaving time is required")]

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
