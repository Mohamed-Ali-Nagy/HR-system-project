using HRSystem.Models;
using HRSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using HRSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRSystem.Controllers
{
    public class AttendanceController : Controller
    {
        IGeneralSettingRepository settingRepository;
        IEmployeeRepositry employeeRepo;
        IDepartmentRepository departmentRepo;
        IAttendanceRepository AttendanceRepository;
       
        HRContext db;
        public AttendanceController(
            IAttendanceRepository _attendanceRepository,
            IEmployeeRepositry _employeeRepo,
            IDepartmentRepository _departmentRepository,
            IGeneralSettingRepository _settingRepository,
        HRContext _db
            )
        {
            AttendanceRepository = _attendanceRepository;
            employeeRepo = _employeeRepo;
            departmentRepo = _departmentRepository;
            settingRepository= _settingRepository;
            db = _db;

        }

        public IActionResult GetempNamesBydept(int depid)
        {
            List<Employee> empnams = db.Employees.Where(n => n.DeptID == depid).ToList();
            return Json(empnams);
        }
        public IActionResult Index()
        {
            List<Attendance> attendanceModel = AttendanceRepository.GetAll();
            List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            foreach (var attendance in attendanceModel)
            {
                AttendanceViewModel attvm = new AttendanceViewModel()
                {
                    ID = attendance.ID,
                    TimeAttendance = attendance.TimeAttendance,
                    TimeLeave = attendance.TimeLeave,
                    Date = attendance.Date


                };

                Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attendance.EmpID);

                if (emp != null)
                {
                    attvm.Employee = emp.Name;

                    if (emp.department != null)
                    {
                        attvm.Department= emp.department.Name;


                    }
                }


                allViewModel.Add(attvm);
            }
            ViewBag.AttendanceList = allViewModel;

            return View();
        
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
            string general = settingRepository.GetWeekRest1();
            string general2 = settingRepository.GetWeekRest2();

            DateTime date = newattendance.Date;
            string dayOfWeek = date.ToString("dddd");



            if (dayOfWeek == general || dayOfWeek == general2)
            {
                TempData["Message"] = "You are trying to enter data on a day off!!";
            
                return RedirectToAction("ADD", newattendance );
            }




            if (ModelState.IsValid == true)
            {
                if (newattendance != null)
                {
                    AttendanceRepository.insert(newattendance);
                    AttendanceRepository.save();

                    return RedirectToAction("Index");
                }
                ViewData["empNameList"] = employeeRepo.getAll();
                ViewData["departementlist"] = departmentRepo.getAll();

                return View(newattendance);
            }
            return View();
            
            
        }




        public IActionResult Edit(int id)
        {
            ViewData["empNameList"] = employeeRepo.getAll();
            ViewData["departementlist"] = departmentRepo.getAll();

            Attendance attendance = AttendanceRepository.GetBYId(id);

            return View("ADD", attendance);
        }


        [HttpPost]
        public IActionResult Edit(int id, Attendance attend)
        {

            string general = settingRepository.GetWeekRest1();
            string general2 = settingRepository.GetWeekRest2();

            DateTime date = attend.Date;
            string dayOfWeek = date.ToString("dddd");



            if (dayOfWeek == general || dayOfWeek == general2)
            {
                TempData["Message"] = "You are trying to enter data on a day off!!";
                return RedirectToAction("ADD", attend);
            }


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


        [HttpPost]
        public IActionResult search( searchattendanceViewModel search)
        {
            if (ModelState.IsValid == true)
            {


                List<Attendance> attemp = AttendanceRepository.Getbyempname(search);
                List<Attendance> attdep = AttendanceRepository.getbydepname(search);
                List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

                if (attemp.Count > 0)
                {
                    for (var i = 0; i < attemp.Count; i++)
                    {
                        AttendanceViewModel attvm = new AttendanceViewModel()
                        {
                            ID = attemp[i].ID,
                            TimeAttendance = attemp[i].TimeAttendance,
                            TimeLeave = attemp[i].TimeLeave,
                            Date = attemp[i].Date
                        };
                        Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attemp[i].EmpID);

                        if (emp != null)
                        {
                            attvm.Employee = emp.Name;

                            if (emp.department != null)
                            {
                                attvm.Department= emp.department.Name;
                            }
                        }

                        allViewModel.Add(attvm);
                        ViewBag.AttendanceList = allViewModel;

                    }
                    return View("index");
                }
                else if (attdep.Count > 0)
                {
                    for (var i = 0; i < attdep.Count; i++)
                    {
                        AttendanceViewModel attvm = new AttendanceViewModel()
                        {
                            ID = attdep[i].ID,
                            TimeAttendance = attdep[i].TimeAttendance,
                            TimeLeave = attdep[i].TimeLeave,
                            Date = attdep[i].Date
                        };

                        Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attdep[i].EmpID);
                        if (emp != null)
                        {
                            attvm.Employee= emp.Name;

                            if (emp.department != null)
                            {
                                attvm.Department= emp.department.Name;
                            }
                        }

                        allViewModel.Add(attvm);
                        ViewBag.AttendanceList = allViewModel;
                    }
                    
                    return View("Index");
                }


                else
                { return Json(false); }
            }
            else
                return RedirectToAction("Index");
        }
      /*  public IActionResult Searchbydate(DateTime fromdate ,DateTime todate)
        {
            List<Attendance> attdate=AttendanceRepository.getbydate(fromdate , todate);
            List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            for (var i = 0; i < attdate.Count; i++)
            {
                AttendanceViewModel attvm = new AttendanceViewModel()
                {
                    ID = attdate[i].ID,
                    TimeAttendance = attdate[i].TimeAttendance,
                    TimeLeave = attdate[i].TimeLeave,
                    Date = attdate[i].Date
                };

                Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attdate[i].EmpID);
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
                return View("index", allViewModel);
        }


        static DateTime fromdatemethod()
        {
            return new DateTime(1997, 6, 2, 00, 00, 0);
        }
        static DateTime todatemethod()
        {
            return  DateTime.Now;
        }
        [HttpPost]
        public IActionResult search (string name , DateTime? fromdate , DateTime?  todate )
        {
           

            if (todate == null) { todate = todatemethod(); }
            if (fromdate == null) { fromdate =fromdatemethod(); }

            List<Attendance> attemp = AttendanceRepository.Getbyempname(name,fromdate,todate);
            List<Attendance> attdep = AttendanceRepository.getbydepname(name, fromdate, todate);
            List<AttendanceViewModel> allViewModel = new List<AttendanceViewModel>();

            if (attemp.Count > 0)
            {
                for (var i = 0; i < attemp.Count; i++)
                {
                    AttendanceViewModel attvm = new AttendanceViewModel()
                    {
                        ID = attemp[i].ID,
                        TimeAttendance = attemp[i].TimeAttendance,
                        TimeLeave = attemp[i].TimeLeave,
                        Date = attemp[i].Date
                    };
                    Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attemp[i].EmpID);

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
                return View("index", allViewModel);
            }
            else if (attdep.Count > 0)
            {
                for (var i = 0; i < attdep.Count; i++)
                {
                    AttendanceViewModel attvm = new AttendanceViewModel()
                    {
                        ID = attdep[i].ID,
                        TimeAttendance = attdep[i].TimeAttendance,
                        TimeLeave = attdep[i].TimeLeave,
                        Date = attdep[i].Date
                    };

                    Employee emp = db.Employees.Include(n => n.department).FirstOrDefault(x => x.Id == attdep[i].EmpID);
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
                return View("Index", allViewModel);
            }


            else
            {
              //  return MappingAttendanceToEmpAttedVM(allViewModel);

                return Json(false); }
        }*/
      public IActionResult Testenddate (DateTime todate, DateTime fromdate )
        {
            if(todate>=fromdate)
            {
                return Json(true);

            }
            else
            { return Json(false); }

        }
        public IActionResult Testdate (DateTime date)
        {
            if(date >= new DateTime(2008, 1, 1, 00, 00, 0))
            {
                return Json(true);
            }
            else
                return Json(false);
        }
        public IActionResult Testtime(DateTime leavetime, DateTime attendancetime)
        {
            if(leavetime >= attendancetime)
                return Json(true);
            else
                return Json(false);
        }

    }
   
}
      