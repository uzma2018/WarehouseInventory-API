using Microsoft.AspNetCore.Mvc;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Services;
using WarehouseInventory.API.Services.Results;

namespace WarehouseInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct([FromRoute]int id)
        {
            if(id <= 0)
            {
                return BadRequest("Product ID must be greater than zero");
            }

            var product = await _productService.GetAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} was not found");
            }

            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody]CreateProductDTO createProduct)
        {
            var result = await _productService.CreateAsync(createProduct);
            var productDto = result.ProductDTO;

            switch (result.Status)
            {
                case ProductCreateStatus.CategoryNotExists:
                    return NotFound($"Product with CategoryID {createProduct.CategoryId} was not found");
                case ProductCreateStatus.Success: 
                    return CreatedAtAction(nameof(GetProduct),new {id = productDto!.Id }, productDto);
                default: return StatusCode(500);

            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct([FromRoute]int id,[FromBody] UpdateProductDTO updateProduct)
        {
            if (id <= 0)
            {
                return BadRequest($"Product ID must be greater than zero");
            }
            var result = await _productService.UpdateAsync(id,updateProduct);

            switch(result.Status)
            {
                case ProductUpdateStatus.NotFound:
                    return NotFound($"Product with ID {id} was not found");
                case ProductUpdateStatus.CategoryNotExists:
                    return NotFound($"Product with CategoryID {updateProduct.CategoryId} was not found");
                case ProductUpdateStatus.Success: 
                    return Ok(result.ProductDTO);
                default: return StatusCode(500);

            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute]int id)
        {
            if(id <= 0)
            {
                return BadRequest("Product ID must be greater than zero");
            }
            var result = await _productService.DeleteAsync(id);
            
            switch(result)
            {
                case ProductDeleteStatus.NotFound:
                    return NotFound($"Product with ID {id} was not found");
                case ProductDeleteStatus.Success:
                    return NoContent();
                default: 
                    return StatusCode(500);
            }
                
            }

        
    }
}
