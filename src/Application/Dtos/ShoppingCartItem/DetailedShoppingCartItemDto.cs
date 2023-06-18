using Application.Dtos.ProductDtos;

namespace Application.Dtos.ShoppingCartItem;

public class DetailedShoppingCartItemDto
{
    public int Id { get; set; }
    public decimal TotalCost { get; set; }
    public int Count { get; set; }
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public DetailedProductDto Product { get; set; }

}
