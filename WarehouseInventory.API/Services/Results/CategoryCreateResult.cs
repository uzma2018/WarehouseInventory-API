using WarehouseInventory.API.DTOs;

namespace WarehouseInventory.API.Services.Results
{
    public class CategoryCreateResult
    {
        public CategoryCreateStatus Status { get; set; }
        public CategoryDTO? CategoryDTO { get; set; }
    }
}
