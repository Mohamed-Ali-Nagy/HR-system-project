using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HRSystem.Models.Validation
{
    public class UniquenameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<HRContext>();

            if (value != null && value is DateTime date)
            {
                var currentItem = validationContext.ObjectInstance as Holidays;
                if (currentItem != null && dbContext.Holidays.Any(h => h.Date == date ))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
