using Application.Dtos.ShoppingCartItemDto;

namespace Application.Contracts
{
    public interface IShoppingCartServices
    {
        public List<ShoppingCartItemDto> GetShoppingCartItems();
        public void AddShoppingCartItem(ShoppingCartItemDto item);
        public void RemoveShoppingCartItem(int id);
        public void DeleteShoppingCartItem(int id);
        public void EditShoppingCartItem(int id);
    }
}
