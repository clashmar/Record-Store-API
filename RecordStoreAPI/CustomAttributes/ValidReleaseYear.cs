using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Validation
{
    public class ValidReleaseYear : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is int year)
            {
                if (year < 1860) return new ValidationResult("Please enter a year after 1860.");
            }
            
            return ValidationResult.Success;
        }
    }
}
