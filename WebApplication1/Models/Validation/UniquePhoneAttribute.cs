using HRSystem.Repository;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models.Validation
{
    public class UniquePhoneAttribute:ValidationAttribute
    {
        HRContext HRdb;
        public UniquePhoneAttribute()
        {
            HRdb = new HRContext();

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                Employee empFrom = validationContext.ObjectInstance as Employee;
                string phone = value.ToString();
                Employee empFromDatabase = HRdb.Employees.FirstOrDefault(p=>p.Phone == phone);
                if (empFromDatabase != null || empFrom.Id == empFromDatabase.Id)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("This phone is already existed");
            }
            return new ValidationResult("The phone number is required");
           
        }
    }
}
