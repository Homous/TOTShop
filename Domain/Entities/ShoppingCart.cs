namespace Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public decimal TotalCost { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
