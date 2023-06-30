using System.ComponentModel.DataAnnotations;

namespace WindPowerPlatformAPI.Infrastructure.Attributes
{
    public class RequiredZeroOrGreaterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Provided value is null.");
            }

            if (!float.TryParse(value.ToString(), out float parsedValue))
            {
                return new ValidationResult("Provided value is not a number.");
            }

            if (parsedValue < 0)
            {
                return new ValidationResult("Provided value is less than zero.");
            }

            return ValidationResult.Success;
        }
    }
}
