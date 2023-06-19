﻿using HRSystem.Models;
using HRSystem.Repository;
using HRSystem.ViewModels;
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
            return View();
        }
        [HttpPost]
        public IActionResult add(Employee employee)
        {

        }


    }
}
