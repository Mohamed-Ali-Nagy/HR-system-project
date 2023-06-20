using HRSystem.Models;

namespace HRSystem.Repository
{
    public class EmployeeRepository:IEmployeeRepositry
    {
        HRContext HRdb;
        public EmployeeRepository(HRContext HRdb)
        {
            this.HRdb = HRdb;
        }

        public void add(Employee employee)
        {
            HRdb.Employees.Add(employee);
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> getAll()
        {
            return HRdb.Employees.ToList();
        }

        public Employee getById(int id)
        {
            return HRdb.Employees.FirstOrDefault(e => e.Id == id);

        }

       

        public List<Employee> getByName(string name)
        {
            return HRdb.Employees.Where(e => e.Name.Contains(name)).ToList();
        }

        public Employee getByPhone(string phone)
        {
            return HRdb.Employees.FirstOrDefault(e => e.Phone == phone);
        }

        public void save()
        {
            HRdb.SaveChanges();
        }

        public Employee update(Employee employee, int id)
        {
            throw new NotImplementedException();
        }


    }
}
