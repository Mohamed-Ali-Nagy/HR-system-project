using HRSystem.Models;
using Microsoft.EntityFrameworkCore;
using  HRSystem.ViewModels;
using System.Linq;

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
        
    }
}
