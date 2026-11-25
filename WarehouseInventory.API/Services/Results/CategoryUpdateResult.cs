using WarehouseInventory.API.DTOs;

namespace WarehouseInventory.API.Services.Results
{
    public class CategoryUpdateResult
    {
        public CategoryUpdateStatus Status { get; set; }
        public CategoryDTO? CategoryDTO { get; set; }
    }
}
