namespace HRSystem.Models
{
    public class GeneralSettings
    {
        public int Id {  get; set; }
        public double AddHourRate { get; set; }
        public double DeducateHourRate { get; set;}
        public string? WeekRest1 { get; set;}
        public string? WeekRest2 { get; set;}

    }
    [Flags]
    public enum WeekRest
    {
        Friday = 1,
        Saturday =2,
        Sunday =4,
        Monday =6,
        Tuesday =8,
        Wednesday=16,
        Thursday =32
    }
}
