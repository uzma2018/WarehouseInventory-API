using System.ComponentModel.DataAnnotations;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string SKU { get; set; }
        [Required]
        [MaxLength(150)]
        public required string Name { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        [Range(typeof(decimal),"0", "79228162514264337593543950335",MinimumIsExclusive =true)]
        public required decimal Price { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public required int StockQuantity { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public required int ReorderLevel { get; set; }
        [Required]
        public required bool IsActive { get; set; } = true;
        [Required]
        public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        [Required]
        public required int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public List<StockTransaction> StockTransactions { get; set; } = new();
    }
}
