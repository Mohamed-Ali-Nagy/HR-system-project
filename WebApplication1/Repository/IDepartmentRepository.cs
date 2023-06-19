using HRSystem.Models;

namespace HRSystem.Repository
{
    public interface IDepartmentRepository
    {
        public List<Department> departments();
        public Department getById(int id);
    }
}
