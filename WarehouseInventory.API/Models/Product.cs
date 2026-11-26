using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string SKU { get; set; }
        public required string Name { get; set; }

        [Range(typeof(decimal),"0", "79228162514264337593543950335",MinimumIsExclusive =true)]
        public required decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public required int StockQuantity { get; set; }
        [Range(0, int.MaxValue)]
        public required int ReorderLevel { get; set; }
        public required bool IsActive { get; set; } = true;
        public required DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public required int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public List<StockTransaction> StockTransactions { get; set; } = new();
    }
}
