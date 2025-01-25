using System.ComponentModel.DataAnnotations;

namespace RecordStoreFrontend.Client.Validation.Attributes
{
    public class ValidReleaseYear : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                if (year < 1860) return new ValidationResult("Please enter a year after 1860.");
            }

            return ValidationResult.Success;
        }
    }
}
