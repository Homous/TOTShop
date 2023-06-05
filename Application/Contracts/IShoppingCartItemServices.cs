using Application.Dtos.ShoppingCartItemDto;

namespace Application.Contracts
{
    public interface IShoppingCartItemServices
    {
        public List<ShoppingCartItemDto> GetShoppingCartItems();
        public int AddShoppingCartItem(ShoppingCartItemDto shoppingCartItem);
        public void DeleteShoppingCartItem(int shoppingCartItemId);
        public ShoppingCartItemDto GetShoppingCartItem(int id);
        public void EditShoppingCartItem(int id, ShoppingCartItemDto shoppingCartItemDto);
    }
}
