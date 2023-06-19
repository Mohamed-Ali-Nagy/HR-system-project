namespace HRSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Employee>? Employees { get; set; } = new List<Employee>();
       // public virtual List<Attendance>? Attendances { get; set; } = new List<Attendance>();

    }
}
