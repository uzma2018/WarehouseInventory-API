using WarehouseInventory.API.DTOs;

namespace WarehouseInventory.API.Services.Results
{
    public class ProductUpdateResult
    {
        public ProductUpdateStatus Status { get; set; }
        public ProductDTO? ProductDTO { get; set; }
    }
}
