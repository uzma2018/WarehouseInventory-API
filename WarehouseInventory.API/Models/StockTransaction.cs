using WarehouseInventory.API.CustomAttributes;

namespace WarehouseInventory.API.Models
{
    public class StockTransaction
    {
        public int Id { get; set; }
        [NotZero]
        public required int QuantityChange { get; set; }
        public required TransactionType TransactionType { get; set; }
        public required DateTime TransactionDateUtc { get; set; } = DateTime.UtcNow;
        public required int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public required int PerformedByUserId { get; set; }
        public User User { get; set; } = default!;

    }
}
