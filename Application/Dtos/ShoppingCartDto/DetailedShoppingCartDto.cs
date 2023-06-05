using Domain.Entities;

namespace Application.Dtos.ShoppingCartDto
{
    public class DetailedShoppingCartDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
