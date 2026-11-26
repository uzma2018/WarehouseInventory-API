namespace WarehouseInventory.API.DTOs
{
    public class ProductDTO
    {
        public required int Id { get; set; }
        public required string SKU { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int StockQuantity { get; set; }
        public required int ReorderLevel { get; set; }
        public bool IsActive { get; set; } = true;
        public required int CategoryId { get; set; }
    }
}
