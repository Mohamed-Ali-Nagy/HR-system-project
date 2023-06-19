using HRSystem.Models;

namespace HRSystem.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        HRContext HRdb;
        public DepartmentRepository(HRContext HRdb)
        {
            this.HRdb = HRdb;
        }
        public List<Department> departments()
        {
            return HRdb.Departments.ToList();
        }

        public Department getById(int id)
        {
            return HRdb.Departments.FirstOrDefault(d => d.Id == id);
        }
    }
}
