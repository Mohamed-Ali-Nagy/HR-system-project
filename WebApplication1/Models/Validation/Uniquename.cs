using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models.Validation
{
    public class Uniquename : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {


            string n = value.ToString();


            HRContext hc = new HRContext();
            Holidays h = hc.Holidays.FirstOrDefault(e => e.Name == n);

            if (h == null)
            {
                return ValidationResult.Success;

            }

            return new ValidationResult("Holiday Exist.");

        }
    }
}
