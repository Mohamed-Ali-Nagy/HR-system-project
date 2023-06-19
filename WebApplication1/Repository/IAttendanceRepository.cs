using HRSystem.Models;

namespace HRSystem.Repository
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetAll();
       
        Attendance GetBYId(int id);
        void insert(Attendance newAttendance);
        void update(int id, Attendance Atten);
        void delete(int id);
        void save();
    }
}
