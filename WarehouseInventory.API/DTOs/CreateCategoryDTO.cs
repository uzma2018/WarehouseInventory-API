using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Name cannot contain only whitespace.")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
