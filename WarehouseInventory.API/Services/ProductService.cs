using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory.API.Data;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Models;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _db.Products.AsNoTracking().Where(p=>p.IsActive).ToListAsync();
            
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetAsync(int id)
        {
            var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
            if (product == null )
            {
                return null;
            }
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductCreateResult> CreateAsync(CreateProductDTO createProduct)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                bool isCategoryExists = await _db.Categories.
               AnyAsync(u => u.Id == createProduct.CategoryId);
                if (!isCategoryExists)
                {
                    return new ProductCreateResult
                    {
                        Status = ProductCreateStatus.CategoryNotExists,
                    };
                }
                var product = new Product
                {
                    Name = createProduct.Name.Trim(),
                    Price = createProduct.Price,
                    ReorderLevel = createProduct.ReorderLevel,
                    CategoryId = createProduct.CategoryId,
                    SKU = $"TEMP{Guid.NewGuid():N}",
                    StockQuantity = 0,
                    IsActive = true,
                    CreatedAtUtc = DateTime.UtcNow,
                };


                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                product.SKU = $"PRD-{product.Id:D6}";
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ProductCreateResult
                {
                    Status = ProductCreateStatus.Success,
                    ProductDTO = _mapper.Map<ProductDTO>(product)
                };
            }
            catch
            {
               await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<ProductUpdateResult> UpdateAsync(int id, UpdateProductDTO updateProduct)
        {
            var product = await _db.Products.FindAsync(id);
            if(product == null || !product.IsActive)
            {
                return new ProductUpdateResult
                {
                    Status = ProductUpdateStatus.NotFound
                };
            }

            bool isCategoryExists = await _db.Categories
                .AnyAsync(u=>u.Id == updateProduct.CategoryId);

            if (!isCategoryExists)
            {
                return new ProductUpdateResult
                {
                    Status = ProductUpdateStatus.CategoryNotExists
                };
            }
            product.Name = updateProduct.Name.Trim();
            product.Price = updateProduct.Price;
            product.Price = updateProduct.ReorderLevel;
            product.Price = updateProduct.CategoryId;
            product.UpdatedAtUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return new ProductUpdateResult
            {
                Status = ProductUpdateStatus.Success,
                ProductDTO = _mapper.Map<ProductDTO>(product)
            };

        }

        public async Task<ProductDeleteStatus> DeleteAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if(product == null || !product.IsActive)
            {
                return ProductDeleteStatus.NotFound;
            }
            product.IsActive = false;
            product.UpdatedAtUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return ProductDeleteStatus.Success;
        }

       
    }
}
