using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryCreateResult> CreateAsync(CreateCategoryDTO createCategory);
        Task<CategoryUpdateResult> UpdateAsync(int id, UpdateCategoryDTO updateCategory);
        Task<CategoryDeleteStatus> DeleteAsync(int id);
    }
}
