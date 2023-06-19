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
            
            ,HRContext _db
            )
        {
            AttendanceRepository = _attendanceRepository;

            db = _db;
        }
        public IActionResult Index()
        {
             List<Attendance> attendanceModel = AttendanceRepository.GetAll();

            List< AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            for (var i=0;i< attendanceModel.Count;i++)
            {
                AttendanceViewModel attvm = new AttendanceViewModel()
                {
                    TimeAttendance = attendanceModel[i].TimeAttendance,
                    TimeLeave = attendanceModel[i].TimeLeave,
                    Date = attendanceModel[i].Date
                  
                };

                attvm.EmployeeName = db.Employees.Where(n => n.ID == attendanceModel[i].EmpID).FirstOrDefault().Name;
                attvm.DepartmentName= db.Departments.Where(n => n.ID == attendanceModel[i].DeptID).FirstOrDefault().Name;

                allViewModel.Add(attvm);
            }
            return View(allViewModel);
        }







        

   

    }
}
