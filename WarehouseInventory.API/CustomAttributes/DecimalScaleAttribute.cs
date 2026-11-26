using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.CustomAttributes
{
    public sealed class DecimalScaleAttribute : ValidationAttribute
    {
        private readonly int _scale;

        public DecimalScaleAttribute(int scale)
        {
            if (scale < 0 || scale > 28)
                throw new ArgumentOutOfRangeException(nameof(scale));

            _scale = scale;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            if(value is not decimal number)
            {
                return new ValidationResult("Invalid decimal value.");
            }
            // Check decimal places (scale)
            int actualScale = (decimal.GetBits(number)[3] >> 16) & 0x7F;

            if (actualScale > _scale)
            {
                return new ValidationResult
                    ($"{validationContext.DisplayName} cannot have more than {_scale} decimal places.");
            }
            return ValidationResult.Success;
        }
    }
}
