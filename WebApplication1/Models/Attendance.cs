using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRSystem.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeAttendance { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime TimeLeave { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        
        [ForeignKey("employee")]
        public int EmpID { get; set; }
        
        public virtual Employee? employee { get; set; }
    }
}
