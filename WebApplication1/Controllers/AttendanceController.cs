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







        

            //AttendanceViewModel allViewModel = new AttendanceViewModel();

            //allViewModel.EmployeeNames = new List<string>();
            //allViewModel.DepartmentNames = new List<string>();

            //foreach (var attendance in attendanceModel)
            //{
            //    if (attendance.employee != null) // Check if employee object is not null
            //    {
            //        allViewModel.EmployeeNames.Add(attendance.employee.Name);
            //        allViewModel.DepartmentNames.Add(attendance.department.Name);
            //    }
            //}

            //// Set other properties of allViewModel as needed
            //if (attendanceModel.Count > 0)
            //{
            //    allViewModel.ID = attendanceModel[0].ID;
            //    allViewModel.TimeAttendance = attendanceModel[0].TimeAttendance;
            //    allViewModel.TimeLeave = attendanceModel[0].TimeLeave;
            //    allViewModel.Date = attendanceModel[0].Date;
            //}

    }
}
