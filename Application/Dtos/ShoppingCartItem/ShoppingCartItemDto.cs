namespace Application.Dtos.ShoppingCartItem
{
    public class ShoppingCartItemDto
    {
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
