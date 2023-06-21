using Application.Dtos.ShoppingCartItem;
using System.Text;

namespace Application.Dtos.ShoppingCart;

public class DetailedShoppingCartDto
{
    public int Id { get; set; }
    public decimal TotalCost { get; set; }
    public List<DetailedShoppingCartItemDto> ShoppingCartItems { get; set; }

    public override string ToString()
    {
        StringBuilder shoppingCartItemsToString = new StringBuilder();
        ShoppingCartItems.ForEach(x => shoppingCartItemsToString.Append(x.ToString()));
        return $"DetailedShoppingCartDto - Id: {Id} - TotalCost: {TotalCost} - ShoppingCartItems: {shoppingCartItemsToString}";
    }
}
