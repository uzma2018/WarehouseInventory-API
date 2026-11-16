using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public List<Product> Products { get; set; } = new();

    }
}
