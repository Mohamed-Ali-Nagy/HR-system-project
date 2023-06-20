using HRSystem.Models;

namespace HRSystem.Repository
{
    public class AttendanceRepository :IAttendanceRepository
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
