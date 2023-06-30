using HRSystem.Models;
using HRSystem.Repository;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepositry employeeRepo;
        IDepartmentRepository departmentRepo;
        public EmployeeController(IEmployeeRepositry employeeRepo,IDepartmentRepository departmentRepository)
        {
            this.employeeRepo = employeeRepo;
            this.departmentRepo = departmentRepository;
        }
        

        public IActionResult Index()
        {
           List<Employee> employees = employeeRepo.getAll();
            return View("Index",employees);
        }
        public IActionResult getDetails(int id)
        {

            Employee employee = employeeRepo.getById(id);

            EmployeeDepartmentVM employeeDepartmentVM = new EmployeeDepartmentVM()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Phone = employee.Phone,
                Gender = employee.EmpGender,
                Nationality = employee.Nationality,
                Birthdate = employee.BirthDate,
                NationalityId = employee.NationalId,
                Hiredate = employee.HireDate,
                Salary = employee.Salary,
                AttendanceTime = employee.AttendanceTime,
                LeavingTime = employee.LeavingTime,

            };
            if (employee.DeptID != null)
            {
                employeeDepartmentVM.DeptName = departmentRepo.getById((int)employee.DeptID).Name;

            }
            return View("getDetails", employeeDepartmentVM);
        }

        [HttpGet]
        public IActionResult add()
        {
            ViewData["DepartmentList"]=departmentRepo.getAll();
            ViewData["EmpGender"]=new List<Gender>() { Gender.Male,Gender.Female};
            return View();
        }
        [HttpPost]
        public IActionResult add(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                ViewData["DepartmentList"] = departmentRepo.getAll();
                ViewData["EmpGender"] = new List<Gender>() { Gender.Male, Gender.Female };

                return View("add", employee);
            }
            employeeRepo.add(employee);
            employeeRepo.save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            Employee employee = employeeRepo.getById(id);
            ViewData["DepartmentList"] = departmentRepo.getAll();
            ViewData["EmpGender"] = new List<Gender>() { Gender.Male, Gender.Female };
            return View(employee);
        }
        [HttpPost]
        public IActionResult edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                employeeRepo.update(employee);
                employeeRepo.save();
               return RedirectToAction("Index");
            }
            ViewData["DepartmentList"]=departmentRepo.getAll();
            ViewData["EmpGender"] = new List<Gender>() { Gender.Male,Gender.Female };
            return View(employee);
        }
       // [HttpPost]
        public IActionResult delete(int id)
        {
            employeeRepo.delete(id);
            employeeRepo.save();
            return RedirectToAction("index");
        }


    }
}
