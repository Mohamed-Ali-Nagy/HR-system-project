using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models.Validation
{
    public class BirthDateValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("The birh date is required");
            DateTime currentTime = DateTime.Now;
            DateTime empBirthDate;
            if (DateTime.TryParse(value.ToString(), out empBirthDate))
            {
                TimeSpan span = currentTime.Subtract(empBirthDate);
                int years = (int)span.TotalDays / 365;
                //  int monthes=(int)(span.TotalDays%365)/30;
                // int days=(int)(span.TotalDays%365)%30;
                if (years > 20 && years < 55)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The age between 20:55");
                }

            }
            return new ValidationResult("The birth date is in valid");

        }
  
    }
}
