using Microsoft.IdentityModel.Tokens;
using RecordStoreAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Validation
{
    public class PopulatedGenres : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<Genres> list)
            {
                if (list.Count <= 0) return new ValidationResult("Must have at least one genre.");
            }
            return ValidationResult.Success;
        }
    }
}
