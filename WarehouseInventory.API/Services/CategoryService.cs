using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory.API.Data;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Models;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _db.Categories.AsNoTracking().ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(u=>u.Id == id);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryCreateResult> CreateAsync(CreateCategoryDTO createCategory)
        {
            string newName = createCategory.Name.Trim();
            bool nameExists = await _db.Categories
                .AnyAsync(u=>u.Name == newName);
            if(nameExists)
            {
                return new CategoryCreateResult
                {
                    Status = CategoryCreateStatus.NameExists,
                };
            }
            var category = new Category 
            {
                Name = newName,
                Description = createCategory.Description,
                CreatedAtUtc = DateTime.UtcNow,

            };

            await _db.AddAsync(category);
            await _db.SaveChangesAsync();

            return new CategoryCreateResult
            {
                Status = CategoryCreateStatus.Success,
                CategoryDTO = _mapper.Map<CategoryDTO>(category)
            };
        }

        public async Task<CategoryUpdateResult> UpdateAsync(
            int id,
            UpdateCategoryDTO updateCategory)
        {
            var category = await _db.Categories.FindAsync(id);

            if(category == null)
            {
                return new CategoryUpdateResult 
                {
                    Status = CategoryUpdateStatus.NotFound
                };
            }

            string currentName = category.Name.Trim();
            string newName = updateCategory.Name.Trim();

            bool isNameChanged = !string.Equals(
                currentName, newName,StringComparison.OrdinalIgnoreCase);

            if(isNameChanged)
            {
                var isNameExist = await _db.Categories
                    .AnyAsync(u => u.Name == newName && u.Id != id);
                if(isNameExist)
                {
                    return new CategoryUpdateResult 
                    {
                        Status = CategoryUpdateStatus.DuplicateName
                    };
                }
            }
            category.Name = newName;
            category.Description = updateCategory.Description;
            category.UpdatedAtUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return new CategoryUpdateResult 
            { 
                Status = CategoryUpdateStatus.Success,
                CategoryDTO = _mapper.Map<CategoryDTO>(category)
            };
        }

        public async Task<CategoryDeleteStatus> DeleteAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if(category == null)
            {
                return CategoryDeleteStatus.NotFound;
            }

            bool hasProducts = await _db.Products
                .AnyAsync(u=>u.CategoryId == category.Id);

            if(hasProducts)
            {
                return CategoryDeleteStatus.HasProducts;
            }

           _db.Categories.Remove(category);
           await _db.SaveChangesAsync();

           return CategoryDeleteStatus.Success;
        }
    }
}
