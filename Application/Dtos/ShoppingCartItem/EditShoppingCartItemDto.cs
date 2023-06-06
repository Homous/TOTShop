namespace Application.Dtos.ShoppingCartItem
{
    public class EditShoppingCartItemDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
    }
}
