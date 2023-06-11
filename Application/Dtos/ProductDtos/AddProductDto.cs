using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ProductDtos
{
    public class AddProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; }
    }
}
