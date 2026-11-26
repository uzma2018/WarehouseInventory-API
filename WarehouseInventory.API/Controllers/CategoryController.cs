using Microsoft.AspNetCore.Mvc;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Services;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController(ICategoryService _categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Category ID must be greater than zero");
            }
            var category= await _categoryService.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound($"Category with ID {id} was not found");
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO createCategory)
        {
            var result = await _categoryService.CreateAsync(createCategory);

            var categoryDto = result.CategoryDTO;
            

            switch (result.Status)
            {
                case CategoryCreateStatus.NameExists :
                    return Conflict(
                        $"A category with the name '{createCategory.Name}' already exists");

                case CategoryCreateStatus.Success:
                    return CreatedAtAction(
                        nameof(GetCategory),
                        new { id = categoryDto!.Id },
                        categoryDto);

                default:
                    return StatusCode(500);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id,UpdateCategoryDTO updateCategory)
        {
            if (id <= 0) 
            {
                return BadRequest("Category ID must be greater than zero");
            }

            var result = await _categoryService.UpdateAsync(id, updateCategory);

            switch(result.Status)
            {
                case CategoryUpdateStatus.NotFound:
                    return NotFound($"Category with ID {id} was not found");
                case CategoryUpdateStatus.DuplicateName:
                    return Conflict(
                       $"A category with the name '{updateCategory.Name}' already exists");
                case CategoryUpdateStatus.Success:
                    return Ok(result.CategoryDTO);
                default:
                    return StatusCode(500);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (id <= 0) 
            {
                return BadRequest("Category ID must be greater than zero");
            }

            var result = await _categoryService.DeleteAsync(id);

            return result switch
            {
                CategoryDeleteStatus.NotFound => NotFound($"Category with ID {id} was not found"),
                CategoryDeleteStatus.HasProducts => Conflict(
                                    "Cannot delete this category because " +
                                    "it is currently linked to active products"),
                CategoryDeleteStatus.Success => NoContent(),
                _ => StatusCode(500),
            };
        }
    }
}
