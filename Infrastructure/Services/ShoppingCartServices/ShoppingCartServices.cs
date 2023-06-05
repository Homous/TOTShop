using Application.Contracts;
using Application.Dtos.ShoppingCartDto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;

namespace Infrastructure.Services.ShoppingCartServices
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddShoppingCart(ShoppingCartDto item)
        {
            var cart = _mapper.Map<ShoppingCart>(item);
            _context.ShoppingCarts.Add(cart);
            _context.SaveChanges();
        }

        public void DeleteShoppingCart(int id)
        {
            var cart = _context.ShoppingCarts.Find(id);
            _context.ShoppingCarts.Remove(cart);
            _context.SaveChanges();
        }

        public void EditShoppingCart(int id, ShoppingCartDto shoppingCartDto)
        {
            var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            if (cart != null)
            {
                _context.ShoppingCarts.Update(cart);
                _context.SaveChanges();
            }

        }

        public ShoppingCartDto GetShoppingCart(int id)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ShoppingCartDto>(cart);
        }

        public List<ShoppingCartDto> GetShoppingCarts()
        {
            var carts = _context.ShoppingCarts.ToList();
            return _mapper.Map<List<ShoppingCartDto>>(carts);
        }

    }
}
