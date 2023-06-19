using HRSystem.Models;

namespace HRSystem.ViewModels
{
    public class EmployeeDepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Gender  Gender { get; set; }
        public double Salary { get; set; }
        public DateTime Hiredate { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthdate { get; set; }
        public string  NationalityId { get; set; }
        public DateTime AttendanceTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public string DeptName { get; set; }

    }
}
