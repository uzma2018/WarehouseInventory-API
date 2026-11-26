using System.Collections;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetAsync(int id);
        Task<ProductCreateResult> CreateAsync(CreateProductDTO createProduct);
        Task<ProductUpdateResult> UpdateAsync(int id,UpdateProductDTO updateProduct);
        Task<ProductDeleteStatus> DeleteAsync(int id);
    }
}
