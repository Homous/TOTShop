using Application.Dtos.ProductDtos;

namespace Application.Dtos.ShoppingCartItem;

public class ShoppingCartItemDto
{
    public decimal TotalCost { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public DetailedProductDto Product { get; set; }
    public override string ToString()
    {
        return $"ShoppingCartItemDto - TotalCost: {TotalCost} - Count: {Count} - ProductId: {ProductId} - ProductPrice: {Product.Price}";
    }
}
