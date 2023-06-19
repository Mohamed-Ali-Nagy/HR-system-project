using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using HRSystem.ViewModels;

namespace HRSystem.Controllers
{
    public class AttendanceController : Controller
    {
        IAttendanceRepository AttendanceRepository;
        HRContext db;
        public AttendanceController(IAttendanceRepository _attendanceRepository

            , HRContext _db
            )
        {
            AttendanceRepository = _attendanceRepository;

            db = _db;
        }
        public IActionResult Index()
        {
            List<Attendance> attendanceModel = AttendanceRepository.GetAll();

            List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            for (var i = 0; i < attendanceModel.Count; i++)
            {
                AttendanceViewModel attvm = new AttendanceViewModel()
                {
                    TimeAttendance = attendanceModel[i].TimeAttendance,
                    TimeLeave = attendanceModel[i].TimeLeave,
                    Date = attendanceModel[i].Date

                };

                Employee emp = db.Employees.Where(n => n.Id == attendanceModel[i].EmpID).FirstOrDefault();


                attvm.EmployeeName = emp.Name;

                attvm.DepartmentName = emp.department.Name;

                allViewModel.Add(attvm);
            }
            return View(allViewModel);
        }











    }
}
