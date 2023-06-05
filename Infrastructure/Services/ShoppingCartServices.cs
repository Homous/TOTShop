using Application.Contracts;
using Application.Dtos.ShoppingCartItemDto;
using Infrastructure.DB;

namespace Infrastructure.Services
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddShoppingCartItem(ShoppingCartItemDto item)
        {
            throw new NotImplementedException();
        }

        public void DeleteShoppingCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public void EditShoppingCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItemDto> GetShoppingCartItems()
        {
            throw new NotImplementedException();
        }

        public void RemoveShoppingCartItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
