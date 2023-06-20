using HRSystem.Models;

namespace HRSystem.Repository
{
    public interface IDepartmentRepository
    {
        public List<Department> getAll();
        public Department getById(int id);
    }
}
