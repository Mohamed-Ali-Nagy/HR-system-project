using HRSystem.Models;

namespace HRSystem.Repository
{
    public interface IHolidaysRepository
    {
        List<Holidays> GetAll();
        Holidays GetById(int id);
        void Insert(Holidays h);
        void Update(int id, Holidays h);
        void Delete(int id);
        void Save();
    }
}
