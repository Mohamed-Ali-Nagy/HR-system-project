using HRSystem.Models;

namespace HRSystem.Repository
{
    public  class GeneralSettingRepository : IGeneralSettingRepository
    {
        HRContext _context;

        public GeneralSettingRepository(HRContext hRContext)
        {
            _context = hRContext;
        }
        public double GetAddHourRate()
        {
            return _context.GeneralSettings.FirstOrDefault().AddHourRate;
        }

        double IGeneralSettingRepository.GetDeducateHourRate()
        {
            return _context.GeneralSettings.FirstOrDefault().DeducateHourRate;
        }

        public string GetWeekRest1()
        {
            return _context.GeneralSettings.FirstOrDefault().WeekRest1;
        }

        public string GetWeekRest2()
        {
            return _context.GeneralSettings.FirstOrDefault().WeekRest2;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateAddHourRate(double HourRate)
        {
            var generalSettings = _context.GeneralSettings.FirstOrDefault();
            generalSettings.AddHourRate=HourRate;
            

        }

        public void UpdateDeducateHourRate(double HourRate)
        {
            var generalSettings = _context.GeneralSettings.FirstOrDefault();
            generalSettings.DeducateHourRate = HourRate;
            
        }

        public void UpdateWeekRest1(string weekRest)
        {
            var generalSettings = _context.GeneralSettings.FirstOrDefault();
            generalSettings.WeekRest1 = weekRest; 
        }

        public void UpdateWeekRest2(string weekRest)
        {
            var generalSettings = _context.GeneralSettings.FirstOrDefault();
            generalSettings.WeekRest2= weekRest;
        }
    }
}
