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
           Employee emp= HRdb.Employees.Find(id);
            emp.IsDeleted= true;
        }

        public List<Employee> getAll()
        {
            return HRdb.Employees.Where(e=>e.IsDeleted==false).ToList();
        }

        public Employee getById(int id)
        {
            return HRdb.Employees.FirstOrDefault(e => e.Id == id&&e.IsDeleted==false);

        }

       

        public List<Employee> getByName(string name)
        {
            return HRdb.Employees.Where(e => e.Name.Contains(name)&&e.IsDeleted==false).ToList();
        }

   
        public void save()
        {
            HRdb.SaveChanges();
        }

        public void update(Employee employee)
        {
         
            HRdb.Employees.Update(employee);
        }


    }
}
