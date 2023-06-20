using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using HRSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Controllers
{
    public class AttendanceController : Controller
    {

        IEmployeeRepositry employeeRepo;
        IDepartmentRepository departmentRepo;
        IAttendanceRepository AttendanceRepository;
        HRContext db;
        public AttendanceController(
            IAttendanceRepository _attendanceRepository,
            IEmployeeRepositry _employeeRepo, 
            IDepartmentRepository _departmentRepository,

            HRContext _db
            )
        {
            AttendanceRepository = _attendanceRepository;
            employeeRepo = _employeeRepo;
            departmentRepo = _departmentRepository;

            db = _db;

        }

        public IActionResult GetempNamesBydept(int depid)
        {
            List<Employee> empnams = db.Employees.Where(n=>n.DeptID== depid).ToList();
            return Json(empnams);
        }
        public IActionResult Index()
        {
            List<Attendance> attendanceModel = AttendanceRepository.GetAll();
            List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            for (var i = 0; i < attendanceModel.Count; i++)
            {
                AttendanceViewModel attvm = new AttendanceViewModel()
                {
                    ID = attendanceModel[i].ID,
                    TimeAttendance = attendanceModel[i].TimeAttendance,
                    TimeLeave =attendanceModel[i].TimeLeave,
                    Date = attendanceModel[i].Date
                };

                //Employee emp = employeeRepo.getById(attendanceModel[i].EmpID);
               Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attendanceModel[i].EmpID);
              
               

                if (emp != null)
                {
                    attvm.EmployeeName = emp.Name;

                    if (emp.department != null)
                    {
                        attvm.DepartmentName = emp.department.Name;
                    }
                }

                allViewModel.Add(attvm);
            }

            return View(allViewModel);
        }



        public IActionResult ADD()

        {
            ViewData["empNameList"] = employeeRepo.getAll();
            ViewData["departementlist"] = departmentRepo.getAll();
            return View();
        }
        [HttpPost]
        public IActionResult ADD(Attendance newattendance)

        {



            if (newattendance !=null)
            {
                AttendanceRepository.insert(newattendance);
                AttendanceRepository.save();

                return RedirectToAction("Index");
            }
            ViewData["empNameList"] = employeeRepo.getAll();
            ViewData["departementlist"] = departmentRepo.getAll();

            return View( newattendance);
        }



       
        public IActionResult Edit(int id)
        {
            ViewData["empNameList"] = employeeRepo.getAll();
            ViewData["departementlist"] = departmentRepo.getAll();

            Attendance attendance = AttendanceRepository.GetBYId(id);

            return View("ADD", attendance);
        }


        [HttpPost]
        public IActionResult Edit( int id ,Attendance attend)
        {

            if (ModelState.IsValid == true)
            {

                AttendanceRepository.update(id, attend);
                AttendanceRepository.save();
                return RedirectToAction("Index");



            }
            ViewData["empNameList"] = employeeRepo.getAll();
            ViewData["departementlist"] = departmentRepo.getAll();

            return View("ADD", attend);


        }




        public IActionResult Delete(int id)
        {
            Attendance att = AttendanceRepository.GetBYId(id);
           
            if (att == null)
            {
                return Content("not found");
            }
            else
            {
                AttendanceRepository.delete(id);
                AttendanceRepository.save();

                return RedirectToAction("Index");
            }


        }





    }
}
