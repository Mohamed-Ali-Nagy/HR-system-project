namespace HRSystem.Repository
{
    public interface IGeneralSettingRepository
    {
        double GetAddHourRate ();
        double GetDeducateHourRate ();
        void UpdateAddHourRate();
        void UpdateDeducateHourRate();


        public string GetWeekRest1();
        public string GetWeekRest2();
        public void UpdateWeekRest1();
        public void UpdateWeekRest2();

        public void Save();
    }
}
