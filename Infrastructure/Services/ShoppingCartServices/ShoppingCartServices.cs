using Application.Contracts;
using Application.Dtos.ShoppingCart;
using Application.Dtos.ShoppingCartItem;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DB;

namespace Infrastructure.Services.ShoppingCartServices
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IShoppingCartItemServices _shoppingCartItemServices;

        public ShoppingCartServices(ApplicationDbContext context, IMapper mapper, IShoppingCartItemServices shoppingCartItemServices)
        {
            _context = context;
            _mapper = mapper;
            _shoppingCartItemServices = shoppingCartItemServices;
        }
        public int AddShoppingCart(ShoppingCartDto item)
        {
            var cart = _mapper.Map<ShoppingCart>(item);

            _context.ShoppingCarts.Add(cart);
            _context.SaveChanges();

            return cart.Id;
        }

        /* public int AddShoppingCartWithItems(ShoppingCartDto item)
         {
             var cart = _mapper.Map<ShoppingCart>(item);
             if(cart != null)
             {
                 _context.ShoppingCarts.Add(cart);
                 _context.SaveChanges();
             }

             return cart.Id;
         }
 */
        public void DeleteShoppingCart(int id)
        {
            var cart = _context.ShoppingCarts.Find(id);
            _context.ShoppingCarts.Remove(cart);
            _context.SaveChanges();
        }


        public void EditShoppingCart(EditShoppingCartItemDto shoppingCartDto)
        {
            var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            if (cart != null)
            {
                _context.ShoppingCarts.Update(cart);
                _context.SaveChanges();
            }

        }

        public DetailedShoppingCartDto GetShoppingCart(int id)
        {
            return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).FirstOrDefault(x => x.Id == id);
            //var cart = _context.ShoppingCarts.FirstOrDefault(x => x.Id == id);
            //return _mapper.Map<ShoppingCartDto>(cart);
        }

        public List<DetailedShoppingCartDto> GetShoppingCarts()
        {
            return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).ToList();
        }

    }
}
