using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models.Validation
{
    public class HireDateValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Hiring date is Required");
            DateTime currentDate = DateTime.Now;
            DateTime companyDate = new DateTime(2008, 1, 1);
            DateTime hireDate;
            if(DateTime.TryParse(value.ToString(), out hireDate))
            {
                if(hireDate>=currentDate||hireDate<companyDate) 
                {
                    return new ValidationResult("The hire date must be after 2008");
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid date");
            }

        }
    }
}
