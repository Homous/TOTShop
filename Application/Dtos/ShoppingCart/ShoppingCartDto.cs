using Application.Dtos.ShoppingCartItem;

namespace Application.Dtos.ShoppingCart
{
    public class ShoppingCartDto
    {
        public decimal TotalCost { get; set; }
        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
