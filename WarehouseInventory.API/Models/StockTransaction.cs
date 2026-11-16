using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseInventory.API.CustomAttributes;

namespace WarehouseInventory.API.Models
{
    public class StockTransaction
    {
        public int Id { get; set; }
        [Required]
        [NotZero]
        public required int QuantityChange { get; set; }
        public TransactionType TransactionType { get; set; }
        [Required]
        public required DateTime TransactionDateUtc { get; set; } = DateTime.UtcNow;
        [Required]
        public required int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        [Required]
        [ForeignKey(nameof(User))]
        public required int PerformedByUserId { get; set; }
        public LocalUser User { get; set; } = default!;

    }
}
