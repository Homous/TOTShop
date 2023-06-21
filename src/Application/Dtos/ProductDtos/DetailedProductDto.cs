
namespace Application.Dtos.ProductDtos;

public class DetailedProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime AddedDate { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}
