using HRSystem.Enums;
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
        #region For Salary
        int GetCountOfDaysOfAttendenceOfEmp(int id, Month month, int year);
        int GetEmpAddHours(int id, Month month);
        int GetEmpDeducateHours(int id, Month month);
        int GetCountOfDaysOfMonth(Month month);
        #endregion
    }
}
