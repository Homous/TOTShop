using Application.Dtos.ShoppingCartItem;

namespace Application.Contracts;

public interface IShoppingCartItemServices
{
    public List<DetailedShoppingCartItemDto> GetShoppingCartItems();
    public int AddShoppingCartItem(ShoppingCartItemDto shoppingCartItem);
    public bool AddListShoppingCartItems(int shoppingCartId, List<ShoppingCartItemDto> items);
    public void DeleteShoppingCartItem(int shoppingCartItemId);
    public DetailedShoppingCartItemDto GetShoppingCartItem(int id);
    public void EditShoppingCartItem(UpdateShoppingCartItemDto shoppingCartItemDto);
}
