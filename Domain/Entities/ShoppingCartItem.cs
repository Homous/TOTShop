namespace Domain.Entities;

public class ShoppingCartItem : BaseEntity
{
    public decimal TotalCost { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}
