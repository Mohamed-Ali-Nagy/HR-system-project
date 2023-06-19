using HRSystem.Models;
namespace HRSystem.Repository
{
    public interface IGeneralSettingRepository
    {
      
        double GetDeducateHourRate ();
        void UpdateAddHourRate(double HourRate);
        void UpdateDeducateHourRate(double HourRate);


        string GetWeekRest1();
        string GetWeekRest2();
        void UpdateWeekRest1(string weekRest);
        void UpdateWeekRest2(string weekRest);

        void Save();
    }
}
