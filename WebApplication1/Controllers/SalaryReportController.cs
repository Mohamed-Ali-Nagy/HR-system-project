using HRSystem.Enums;
using HRSystem.Models;
using HRSystem.Repository;
using HRSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Globalization;


namespace HRSystem.Controllers
{
    public class SalaryReportController : Controller
    {
        readonly IEmployeeRepositry employeeRepo;
        readonly IGeneralSettingRepository generalSettingRepo;
        readonly IAttendanceRepository attendanceRepo;
        readonly IHolidaysRepository holidaysRepository;

        public SalaryReportController(IEmployeeRepositry employeeRepo, IAttendanceRepository attendanceRepo, IGeneralSettingRepository generalSettingRepo,IHolidaysRepository holidaysRepository)
        {
            this.employeeRepo = employeeRepo;
            this.attendanceRepo = attendanceRepo;
            this.generalSettingRepo = generalSettingRepo;
            this.holidaysRepository = holidaysRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //intialize lst of employess with thier salaries
            List<SalaryReportViewModel> salRebVMlst = new List<SalaryReportViewModel>();
            //get all employess from DB
            List<Employee> employees = employeeRepo.getAll();
            #region Data for pagination
            int count= employees.Count;
            ViewBag.Result = count;
            int recordsPerPages = count / 10;
            ViewBag.recordsPerPages = recordsPerPages;
            #endregion
            #region Current month() and year
            //Get Current month() and year 
           Enums.Month ThisMonth = (Enums.Month)Enum.Parse(typeof(Enums.Month), DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month));
            int thisYear = DateTime.Now.Year;
            #endregion
            #region The the Default (AddHourRate&DeducateRate) From DB
            //Get The the Default (AddHourRate&DeducateRate) From DB
            double DefaultAddHourRate = generalSettingRepo.GetAddHourRate();
            double DefaultDeducateRate = generalSettingRepo.GetDeducateHourRate();
            #endregion
            #region days of current Months 
            int DaysOfMonth = attendanceRepo.GetCountOfDaysOfMonth(ThisMonth);
            #endregion     
            #region count of WeekRest Days in this Months
            //Get count of WeekRest Days in this Months
            int countOfweekRestOfMonth = 0;
            if (generalSettingRepo.GetWeekRest1() != null)
            {
                countOfweekRestOfMonth = 4;
            }
            if (generalSettingRepo.GetWeekRest2() != null)
            {
                countOfweekRestOfMonth = 8;
            }
            #endregion
            #region Get Holidays in this Month
            int holidayOfMonth = holidaysRepository.GetCountOfHolidaysInMonths(DateTime.Now.Month);
            #endregion
            // Get the days of atthendece for all emps and calculate thier salries
            foreach (Employee employee in employees)
            {
                SalaryReportViewModel salRebVM = new();
                salRebVM.Id = employee.Id;
                salRebVM.EmpNme = employee.Name;
                salRebVM.DeptName = employee.department.Name;
                salRebVM.BasicSalary = employee.Salary;
                salRebVM.Attendancedays = attendanceRepo.GetCountOfDaysOfAttendenceOfEmp(employee.Id, ThisMonth, thisYear);
                salRebVM.AbsenceDays = DaysOfMonth-(salRebVM.Attendancedays+countOfweekRestOfMonth+holidayOfMonth);
                salRebVM.AddHours = attendanceRepo.GetEmpAddHours(employee.Id,ThisMonth, thisYear);
                salRebVM.DedacatedHours = attendanceRepo.GetEmpDeducateHours(employee.Id, ThisMonth,thisYear);
                salRebVM.TotalAdd = salRebVM.AddHours * DefaultAddHourRate;
                salRebVM.TotalDedacated = salRebVM.DedacatedHours * DefaultDeducateRate;
                //Calculation Of Daily And hour Rate
                double HourRate = ((employee.Salary) / (30 * 8));
                double dailyRate = ((employee.Salary) / (30));
                //-------------------------------------
                salRebVM.TotalDue = Math.Round((double)(employee.Salary + HourRate * (salRebVM.TotalAdd - salRebVM.TotalDedacated) - (salRebVM.AbsenceDays * dailyRate)), 2);
                salRebVMlst.Add(salRebVM);
            }
            return View(salRebVMlst);
        }
        [HttpPost]
        public IActionResult Index(string name,Month month ,int yaer)
        {
            //intialize lst of employess with thier salaries
            List<SalaryReportViewModel> salRebVMlst = new List<SalaryReportViewModel>();
            //get all employess from DB
            List<Employee> employees = employeeRepo.getByName(name);
            #region Current month() and year
            //Get Current month() and year 
            Enums.Month ThisMonth = month;
            int thisYear = yaer;
            #endregion
            #region The the Default (AddHourRate&DeducateRate) From DB
            //Get The the Default (AddHourRate&DeducateRate) From DB
            double DefaultAddHourRate = generalSettingRepo.GetAddHourRate();
            double DefaultDeducateRate = generalSettingRepo.GetDeducateHourRate();
            #endregion
            #region days of current Months
            //Get Thhe days of current Months 
            int DaysOfMonth = attendanceRepo.GetCountOfDaysOfMonth(ThisMonth);
            #endregion     
            #region count of WeekRest Days in this Months
            //Get count of WeekRest Days in this Months
            int countOfweekRestOfMonth = 0;
            if (generalSettingRepo.GetWeekRest1() != null)
            {
                countOfweekRestOfMonth = 4;
            }
            if (generalSettingRepo.GetWeekRest2() != null)
            {
                countOfweekRestOfMonth = 8;
            }
            #endregion
            #region Get Holidays in this Month
            int holidayOfMonth = holidaysRepository.GetCountOfHolidaysInMonths((int)month);
            #endregion
            // Get the days of atthendece for all emps and calculate thier salries
            foreach (Employee employee in employees)
            {
                SalaryReportViewModel salRebVM = new();
                salRebVM.Id = employee.Id;
                salRebVM.EmpNme = employee.Name;
                salRebVM.DeptName = employee.department.Name;
                salRebVM.BasicSalary = employee.Salary;
                salRebVM.Attendancedays = attendanceRepo.GetCountOfDaysOfAttendenceOfEmp(employee.Id, ThisMonth, thisYear);
                salRebVM.AbsenceDays = (DaysOfMonth-(salRebVM.Attendancedays + countOfweekRestOfMonth+ holidayOfMonth));
                salRebVM.AddHours = attendanceRepo.GetEmpAddHours(employee.Id, ThisMonth,thisYear);
                salRebVM.DedacatedHours = attendanceRepo.GetEmpDeducateHours(employee.Id, ThisMonth, thisYear);
                salRebVM.TotalAdd = salRebVM.AddHours * DefaultAddHourRate;
                salRebVM.TotalDedacated = salRebVM.DedacatedHours * DefaultDeducateRate;
                //Calculation Of Daily And hour Rate
                double HourRate = ((employee.Salary) / (30 * 8));
                double dailyRate = ((employee.Salary) / (30));
                //-------------------------------------
                salRebVM.TotalDue = Math.Round((double)(employee.Salary + HourRate * (salRebVM.TotalAdd - salRebVM.TotalDedacated) - (salRebVM.AbsenceDays * dailyRate)), 2);
                salRebVMlst.Add(salRebVM);
            }
            return View(salRebVMlst);
        }
        public IActionResult Details(int id)
        {
            #region Current month() and year
            //Get Current month() and year 
            Enums.Month ThisMonth = (Enums.Month)Enum.Parse(typeof(Enums.Month), DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month));
            int thisYear = DateTime.Now.Year;
            #endregion
            #region The the Default (AddHourRate&DeducateRate) From DB
            //Get The the Default (AddHourRate&DeducateRate) From DB
            double DefaultAddHourRate = generalSettingRepo.GetAddHourRate();
            double DefaultDeducateRate = generalSettingRepo.GetDeducateHourRate();
            #endregion
            #region days of current Months
            //Get Thhe days of current Months 
            int DaysOfMonth = attendanceRepo.GetCountOfDaysOfMonth(ThisMonth);
            #endregion     
            #region count of WeekRest Days in this Months
            //Get count of WeekRest Days in this Months
            int countOfweekRestOfMonth = 0;
            if (generalSettingRepo.GetWeekRest1() != null)
            {
                countOfweekRestOfMonth = 4;
            }
            if (generalSettingRepo.GetWeekRest2() != null)
            {
                countOfweekRestOfMonth = 8;
            }
            #endregion
            #region Get Holidays in this Month
            int holidayOfMonth = holidaysRepository.GetCountOfHolidaysInMonths(DateTime.Now.Month);
            #endregion
            //Get employee
            Employee employee = employeeRepo.getById(id);
            //Calculation Of Daily And hour Rate
            double HourRate = ((employee.Salary) / (30 * 8));
            double dailyRate = ((employee.Salary) / (30));
            //-------------------------------------
            //Assign to View Mode Of Salary Reprt 
            SalaryReportViewModel salaryReportViewModel = new SalaryReportViewModel
            {
                EmpNme = employee.Name,
                BasicSalary = employee.Salary,
                DeptName = employee.department.Name,
                Month = ThisMonth,
                Attendancedays = attendanceRepo.GetCountOfDaysOfAttendenceOfEmp(employee.Id, ThisMonth, thisYear),
                //AbsenceDays = (DaysOfMonth - (Attendancedays + countOfweekRestOfMonth)),
                AddHours = attendanceRepo.GetEmpAddHours(employee.Id, ThisMonth,thisYear),
                DedacatedHours = attendanceRepo.GetEmpDeducateHours(employee.Id, ThisMonth, thisYear),
                //TotalAdd = AddHours * DefaultAddHourRate,
                //TotalDedacated = DedacatedHours * DefaultDeducateRate
                //salRebVM.TotalDue=
            };
            salaryReportViewModel.AbsenceDays = (DaysOfMonth - (salaryReportViewModel.Attendancedays + countOfweekRestOfMonth+ holidayOfMonth));
            salaryReportViewModel.TotalAdd = salaryReportViewModel.AddHours * DefaultAddHourRate;
            salaryReportViewModel.TotalDedacated = salaryReportViewModel.DedacatedHours * DefaultDeducateRate;
            salaryReportViewModel.TotalDue = Math.Round((double)(employee.Salary + HourRate * (salaryReportViewModel.TotalAdd - salaryReportViewModel.TotalDedacated) - (salaryReportViewModel.AbsenceDays * dailyRate)), 2);

            return View(salaryReportViewModel);
        }


    }
}
