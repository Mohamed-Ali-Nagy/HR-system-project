using HRSystem.Custom_Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    public class GeneralSettings
    {
        public int Id {  get; set; }
        [Required]
        [Range(1,24)]
        public double AddHourRate { get; set; }
        [Required]
        [Range(1, 24)]
        public double DeducateHourRate { get; set;}
        [Required]
        [RegularExpression("(Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)",ErrorMessage = "Please enter a valid week day Saturday.....etc")]
        public string WeekRest1 { get; set;}

        [RegularExpression("(Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday| )", ErrorMessage = "Please enter a valid week day Saturday....etc")]
        //[weekrest2validation]
        [Remote("Validation", "GeneralSetting",AdditionalFields = "WeekRest1",ErrorMessage ="Plese Enter another day")]
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
