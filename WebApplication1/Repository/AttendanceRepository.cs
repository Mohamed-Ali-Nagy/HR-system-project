using HRSystem.Models;
using Microsoft.EntityFrameworkCore;
using  HRSystem.ViewModels;
using System.Linq;
﻿using HRSystem.Enums;
using System.Globalization;


namespace HRSystem.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {

        HRContext db;

        public AttendanceRepository(HRContext _db)
        {
            db = _db;
        }
        public void delete(int id)
        {
            Attendance deleteAttendance = GetBYId(id);

            db.Remove(deleteAttendance);
        }

        public List<Attendance> GetAll()
        {
            return db.Attendances.ToList();
        }
        
        public List<Attendance> Getbyempname(searchattendanceViewModel searchmodel)
        {
           
            List<Employee> emp1 = db.Employees.Where(e => e.Name.Contains(searchmodel.Name)).ToList();
            List<Attendance> atttendance1 = new List<Attendance>();

            foreach (Employee item in emp1)
            {
                Attendance search = db.Attendances.Where(n=>n.Date>= searchmodel.fromdate && n.Date<= searchmodel.todate && n.EmpID== item.Id).FirstOrDefault();
                if (search != null)
                    atttendance1.Add(search);
            }
            return atttendance1;
        }
        public List<Attendance> getbydepname(searchattendanceViewModel searchmodel) 
        {
            List<Department> dep = db.Departments.Where(n => n.Name.Contains(searchmodel.Name)).ToList();
            List<Attendance> att = new List<Attendance>();
            foreach (Department item in dep)
            {
                List<Employee> emp = db.Employees.Where(n => n.DeptID == item.Id ).ToList();
                foreach (Employee item2 in emp)
                {
                   
                    Attendance search = db.Attendances.FirstOrDefault(n => n.EmpID == item2.Id && n.Date >= searchmodel.fromdate && n.Date<= searchmodel.todate);
                    if(search!=null)
                    att.Add(search);

                }

            }
            return att;
        
        }
        public List<Attendance> getbydate(DateTime fromdate ,DateTime todate)
        {
            List<Attendance> att=db.Attendances.Where(n=>n.Date>=fromdate &&n.Date<=todate).ToList();
            return att;

          }

        public Attendance GetBYId(int id)
        {
            return db.Attendances.FirstOrDefault(n => n.ID == id);
        }

        public void insert(Attendance newAttendance)
        {


            db.Attendances.Add(newAttendance);

        }

        public void save()
        {
            db.SaveChanges();
        }

        public void update(int id, Attendance Atten)
        {
            Attendance oldattendance = GetBYId(id);
            oldattendance.ID = Atten.ID;
            oldattendance.TimeAttendance = Atten.TimeAttendance;
            oldattendance.TimeLeave = Atten.TimeLeave;
            oldattendance.Date = Atten.Date;
            oldattendance.EmpID = Atten.EmpID;
        }
        public int GetCountOfDaysOfAttendenceOfEmp(int id,Month month,int year)
        {
            //..Where(x => ).
            var monthNumber =Convert.ToInt32(month);
            int days = db.Attendances.Include(n => n.employee).Where(x=>x.employee.Id == id & x.Date.Month == monthNumber & x.Date.Year == year).Count();
            return days;
        }
        public int GetEmpAddHours(int id,Month month, int year) 
        {
            int AddHours = 0;
            var monthNumber = Convert.ToInt32(month);
            var DefaultAttendeceTime =db.Employees.FirstOrDefault(x=>x.Id==id).AttendanceTime.Hour;
            var DefaultLeavingTime =db.Employees.FirstOrDefault(x=>x.Id==id).LeavingTime.Hour;
            List<Attendance> attendances=db.Attendances.Where(x=>x.EmpID==id&x.Date.Month== monthNumber &x.Date.Year==year).ToList();
            foreach (var item in attendances)
            {
               if(item.TimeAttendance.Hour<DefaultAttendeceTime)
               {
                    AddHours = AddHours + (DefaultAttendeceTime - item.TimeAttendance.Hour);
               }
               if (item.TimeLeave.Hour > DefaultLeavingTime)
               {
                   AddHours = AddHours + (item.TimeLeave.Hour - DefaultLeavingTime);
               }
            }
            return AddHours;
        }
        public int GetEmpDeducateHours(int id, Month month, int year)
        {
            int DeducateHours = 0;
            var monthNumber = Convert.ToInt32(month);
            var DefaultAttendeceTime = db.Employees.FirstOrDefault(x => x.Id == id).AttendanceTime.Hour;
            var DefaultLeavingTime = db.Employees.FirstOrDefault(x => x.Id == id).LeavingTime.Hour;
            List<Attendance> attendances = db.Attendances.Where(x => x.EmpID == id & x.Date.Month == monthNumber).ToList();
            foreach (var item in attendances)
            {
                if (item.TimeAttendance.Hour > DefaultAttendeceTime)
                {
                    DeducateHours = DeducateHours + (item.TimeAttendance.Hour- DefaultAttendeceTime);
                }
                if (item.TimeLeave.Hour < DefaultLeavingTime)
                {
                    DeducateHours = DeducateHours + (item.TimeLeave.Hour - DefaultLeavingTime);
                }
            }
            return DeducateHours;
        }

        public int GetCountOfDaysOfMonth(Month month)
        {
            Month ThisMonth = (Month)Enum.Parse(typeof(Month), DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month));

            int daysMonths;
            if (ThisMonth == Month.January || ThisMonth == Month.March || ThisMonth == Month.July || ThisMonth == Month.August || ThisMonth == Month.October || ThisMonth == Month.December)
            {
                daysMonths = 31;
            }
            else if (ThisMonth == Month.April || ThisMonth == Month.June || ThisMonth == Month.September || ThisMonth == Month.November)
            {
                daysMonths = 30;
            }
            else if (ThisMonth == Month.February && DateTime.Now.Year % 4 == 0)
            {
                daysMonths = 29;
            }
            else
            {
                daysMonths = 28;
            }
            return daysMonths;
        }
    }
}
