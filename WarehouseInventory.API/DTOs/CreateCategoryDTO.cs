using System.ComponentModel.DataAnnotations;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
