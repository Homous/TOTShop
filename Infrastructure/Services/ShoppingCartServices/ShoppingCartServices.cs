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

        public ShoppingCartServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int AddShoppingCart(ShoppingCartDto item)
        {
            try
            {
                var cart = _mapper.Map<ShoppingCart>(item);

                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();

                return cart.Id;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddingShoppingCart has failed {ex.Message}");
                Console.WriteLine(ex);
                return -1;
            }
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
            try
            {
                var cart = _context.ShoppingCarts.Find(id);
                _context.ShoppingCarts.Remove(cart);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteShoppingCart has failed {ex.Message}");
                Console.WriteLine(ex);
            }
        }


        public void EditShoppingCart(EditShoppingCartItemDto shoppingCartDto)
        {
            try
            {
                var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
                if (cart != null)
                {
                    _context.ShoppingCarts.Update(cart);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EditShoppingCart has failed {ex.Message}");
                Console.WriteLine(ex);
            }
        }

        public void EditShoppingCart(DetailedShoppingCartDto shoppingCartDto)
        {
            try
            {
                var cart = _mapper.Map<ShoppingCart>(shoppingCartDto);
                var cartDb = _context.ShoppingCarts.FirstOrDefault(x => x.Id == shoppingCartDto.Id);
                cartDb.TotalCost = cart.TotalCost;
                //cartDb.ShoppingCartItems = cart.ShoppingCartItems;

                if (cart != null)
                {
                    _context.ShoppingCarts.Update(cartDb);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EditShoppingCart has failed {ex.Message}");
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public DetailedShoppingCartDto GetShoppingCart(int id)
        {
            try
            {
                return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetShoppingCart has failed {ex.Message}");
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<DetailedShoppingCartDto> GetShoppingCarts()
        {
            try
            {
                return _mapper.ProjectTo<DetailedShoppingCartDto>(_context.ShoppingCarts).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetShoppingCarts has failed {ex.Message}");
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
