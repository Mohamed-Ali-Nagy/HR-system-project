using HRSystem.Models;

namespace HRSystem.Repository
{
    public class HolidaysRepository : IHolidaysRepository
    {
        private readonly HRContext context;
        public HolidaysRepository(HRContext context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            var holiday = GetById(id);
            context.Holidays.Remove(holiday);
        }
        public List<Holidays> GetAll()
        {
            return context.Holidays.ToList();
        }
        public Holidays GetById(int id)
        {
            return context.Holidays.FirstOrDefault(x => x.id == id);
        }
        public void Insert(Holidays holiday)
        {
            context.Holidays.Add(holiday);

        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(int id, Holidays holiday)
        {
            var HModel = context.Holidays.FirstOrDefault(x => x.id == id);
            HModel.Name = holiday.Name;
            HModel.Date = holiday.Date;
        }

        public int GetCountOfHolidaysInMonths(int months)
        {
            int CountOfHolidaysInMonths=context.Holidays.Where(x=>x.Date.Month==months).Count();
            return CountOfHolidaysInMonths;
        }

    }
}
