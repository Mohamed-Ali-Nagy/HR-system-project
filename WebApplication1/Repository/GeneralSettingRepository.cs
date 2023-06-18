using HRSystem.Models;

namespace HRSystem.Repository
{
    public class GeneralSettingRepository : IGeneralSettingRepository
    {
        HRContext _context;

        public GeneralSettingRepository(HRContext hRContext)
        {
            _context = hRContext;
        }
        double IGeneralSettingRepository.GetAddHourRate()
        {
            _context.GeneralSettings.FirstOrDefault(x => x.AddHourRate);
        }

        double IGeneralSettingRepository.GetDeducateHourRate()
        {
            throw new NotImplementedException();
        }

        string IGeneralSettingRepository.GetWeekRest1()
        {
            throw new NotImplementedException();
        }

        string IGeneralSettingRepository.GetWeekRest2()
        {
            throw new NotImplementedException();
        }

        void IGeneralSettingRepository.Save()
        {
            throw new NotImplementedException();
        }

        void IGeneralSettingRepository.UpdateAddHourRate()
        {
            throw new NotImplementedException();
        }

        void IGeneralSettingRepository.UpdateDeducateHourRate()
        {
            throw new NotImplementedException();
        }

        void IGeneralSettingRepository.UpdateWeekRest1()
        {
            throw new NotImplementedException();
        }

        void IGeneralSettingRepository.UpdateWeekRest2()
        {
            throw new NotImplementedException();
        }
    }
}
