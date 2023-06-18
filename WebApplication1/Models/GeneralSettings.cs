namespace HRSystem.Models
{
    public class GeneralSettings
    {
        public int Id {  get; set; }
        public double AddHourRate { get; set; }
        public double DeducateHourRate { get; set;}
        public WeekRest? WeekRest1 { get; set;}
        public WeekRest? WeekRest2 { get; set;}

    }
    public enum WeekRest
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
}
