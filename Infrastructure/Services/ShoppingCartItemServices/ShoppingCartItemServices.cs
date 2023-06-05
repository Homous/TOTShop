using Application.Contracts;
using Application.Dtos.ShoppingCartItemDto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;

namespace Infrastructure.Services.ShoppingCartItemItemServices
{
    public class ShoppingCartItemServices : IShoppingCartItemServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartItemServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int AddShoppingCartItem(ShoppingCartItemDto item)
        {
            var CartItem = _mapper.Map<ShoppingCartItem>(item);
            _context.ShoppingCartItems.Add(CartItem);
            _context.SaveChanges();

            return CartItem.Id;
        }

        public void DeleteShoppingCartItem(int id)
        {
            var CartItem = _context.ShoppingCartItems.Find(id);
            _context.ShoppingCartItems.Remove(CartItem);
            _context.SaveChanges();
        }

        public void EditShoppingCartItem(int id, ShoppingCartItemDto shoppingCartItemDto)
        {
            var CartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemDto);
            if (CartItem != null)
            {
                _context.ShoppingCartItems.Update(CartItem);
                _context.SaveChanges();
            }

        }
        public ShoppingCartItemDto GetShoppingCartItem(int id)
        {
            var cart = _context.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ShoppingCartItemDto>(cart);
        }
        public List<ShoppingCartItemDto> GetShoppingCartItems()
        {
            var CartItems = _context.ShoppingCartItems.ToList();
            return _mapper.Map<List<ShoppingCartItemDto>>(CartItems);
        }

    }
}
