using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HRSystem.Models.Validation
{
    public class UniquenameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<HRContext>();

            if (value is DateTime date)
            {
                var currentItem = validationContext.ObjectInstance as Holidays;
                if (currentItem == null)
                {
                    return ValidationResult.Success;
                }

                var existingItem = dbContext.Holidays.FirstOrDefault(h => h.id != currentItem.id && h.Date == date);
                if (existingItem != null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
