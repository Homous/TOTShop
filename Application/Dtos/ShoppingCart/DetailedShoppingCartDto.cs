using Application.Dtos.ShoppingCartItem;

namespace Application.Dtos.ShoppingCart
{
    public class DetailedShoppingCartDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public List<DetailedShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
