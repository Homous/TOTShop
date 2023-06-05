using Domain.Entities;

namespace Application.Contracts
{
    public interface IShoppingCartItemServices
    {
        public List<ShoppingCartItem> GetShoppingCartItems();
        public void AddShoppingCartItem(ShoppingCartItem shoppingCartItem);
        public void RemoveShoppingCartItem(int shoppingCartItemId);
        public void RemoveShoppingCartItem(string shoppingCartItemId);
        public ShoppingCartItem GetShoppingCartItem(int shoppingCartItemId);
    }
}
