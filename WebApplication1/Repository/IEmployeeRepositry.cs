using HRSystem.Models;
namespace HRSystem.Repository
{
    public interface IEmployeeRepositry
    {
        public Employee getById(int id);
        public List<Employee> getAll();
        public List<Employee> getByName(string name);

        public void add(Employee employee);

        public Employee update(Employee employee,int id);
        public void delete(int id);
        public void save();

    }
}
