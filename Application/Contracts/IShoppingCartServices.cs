using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;

namespace Application.Contracts
{
    public interface IShoppingCartServices
    {
        public List<DetailedShoppingCartDto> GetShoppingCarts();
        public DetailedShoppingCartDto GetShoppingCart(int id);
        public int AddShoppingCart(ShoppingCartDto item);
        public void DeleteShoppingCart(int id);
        public void EditShoppingCart(EditShoppingCartItemDto shoppingCartDto);
    }
}
