using System.ComponentModel.DataAnnotations;

namespace WarehouseInventory.API.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public required string PasswordHash { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Role { get; set; }
        [Required]
        public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public List<StockTransaction> StockTransactions { get; set; } = new();
       
    }
}
