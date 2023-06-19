using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [ForeignKey("employee")]
        public int EmpId { get; set; }
        public virtual Employee? employee { get; set; }
    }
}
