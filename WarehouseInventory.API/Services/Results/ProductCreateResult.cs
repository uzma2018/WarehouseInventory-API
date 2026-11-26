using WarehouseInventory.API.DTOs;

namespace WarehouseInventory.API.Services.Results
{
    public class ProductCreateResult
    {
        public ProductCreateStatus Status { get; set; }
        public ProductDTO? ProductDTO { get; set; }
    }
}
