using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.ProductDTOS
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
    }
}
