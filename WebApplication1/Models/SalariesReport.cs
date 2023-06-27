using HRSystem.Enums;

namespace HRSystem.Models
{
    public class SalariesReport
    {
        public int Id { get; set; } 

        public int EmployId { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }

        public double Salary { get; set; }
    }
}
