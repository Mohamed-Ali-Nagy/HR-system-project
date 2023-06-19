using Microsoft.Build.Framework;

namespace HRSystem.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Employee>? employees { get; set; } = new List<Employee>();

        public List<Attendance>? attendances { get; set; } = new List<Attendance>();
    }
}
