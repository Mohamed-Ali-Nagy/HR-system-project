using HRSystem.Models;

namespace HRSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepositry
    {
        HRContext HRdb;
        public EmployeeRepository(HRContext HRdb)
        {
            this.HRdb = HRdb;
        }
        public Employee get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
