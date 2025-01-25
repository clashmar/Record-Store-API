using RecordStoreFrontend.Client.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreFrontend.Client.Validation.Attributes
{
    public class PopulatedGenres : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<GenreEnum> list)
            {
                if (list.Count <= 0) return new ValidationResult("Must have at least one genre.");
            }

            return ValidationResult.Success;
        }
    }
}
