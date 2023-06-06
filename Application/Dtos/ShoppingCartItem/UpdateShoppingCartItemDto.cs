namespace Application.Dtos.ShoppingCartItem
{
    public class UpdateShoppingCartItemDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
