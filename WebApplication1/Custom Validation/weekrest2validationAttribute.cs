using HRSystem.Models;
using HRSystem.Repository;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Custom_Validation
{
    public class weekrest2validationAttribute : ValidationAttribute
    {
        IGeneralSettingRepository generalSettingRepository;

        public weekrest2validationAttribute(IGeneralSettingRepository _generalSettingRepository)
        {
            generalSettingRepository = _generalSettingRepository;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var obj = generalSettingRepository.GetWeekRest1().ToString();
            if(value != obj)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Please Enter a different day");
        }

    }
}
