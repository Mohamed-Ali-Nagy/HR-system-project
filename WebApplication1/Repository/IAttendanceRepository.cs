using HRSystem.Models;
using HRSystem.ViewModels;

namespace HRSystem.Repository
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetAll();
        List<Attendance> Getbyempname(searchattendanceViewModel search);
        List<Attendance> getbydepname(searchattendanceViewModel search);
        List<Attendance> getbydate(DateTime fromdate, DateTime todate);

       Attendance GetBYId(int id);
        void insert(Attendance newAttendance);
        void update(int id, Attendance Atten);
        void delete(int id);
        void save();
    }
}
