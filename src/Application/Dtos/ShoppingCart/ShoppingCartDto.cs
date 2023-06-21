using Application.Dtos.ShoppingCartItem;
using System.Text;

namespace Application.Dtos.ShoppingCart;

public class ShoppingCartDto
{
    public decimal TotalCost { get; set; }
    public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    public override string ToString()
    {
        StringBuilder shoppingCartItemsToString = new StringBuilder();
        ShoppingCartItems.ForEach(x => shoppingCartItemsToString.Append(x.ToString()));
        return $"ShoppingCartDto - TotalCost: {TotalCost} - ShoppingCartItems: {shoppingCartItemsToString}";
    }
}
