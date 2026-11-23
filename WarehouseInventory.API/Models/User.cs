namespace WarehouseInventory.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public List<StockTransaction> StockTransactions { get; set; } = new();
       
    }
}
