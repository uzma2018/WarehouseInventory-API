using System.ComponentModel.DataAnnotations;
using WarehouseInventory.API.CustomAttributes;

namespace WarehouseInventory.API.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [MaxLength(150)]
        [RegularExpression(@".*\S.*",
            ErrorMessage = "Name cannot contain only whitespace.")]
        public string Name { get; set; } = string.Empty;


        [Range(typeof(decimal), "0", "9999999999999999.99")]
        [DecimalScale(2)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }
        [Range(1,int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
