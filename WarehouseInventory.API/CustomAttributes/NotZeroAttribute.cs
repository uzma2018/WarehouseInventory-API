using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.CustomAttributes
{
    public sealed class NotZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            if(value is not int quantity)
            {
                return new ValidationResult("Invalid quantity value.");
            }
            if(quantity == 0)
            {
                return new ValidationResult
                    (ErrorMessage ?? $"{validationContext.DisplayName} cannot be zero.");
            }
            return ValidationResult.Success;
        }
    }
}
