using Application.Contracts;
using Application.Dtos.ShoppingCartItem;
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

        public bool AddListShoppingCartItems(int shoppingCartId, List<ShoppingCartItemDto> items)
        {
            var cart = _context.ShoppingCarts.Find(shoppingCartId);
            if (cart != null)
            {
                var shoppingCartitems = _mapper.Map<List<ShoppingCartItem>>(items).ToList();
                shoppingCartitems.ForEach(item => item.ShoppingCartId = shoppingCartId);

                _context.ShoppingCartItems.AddRange(shoppingCartitems);
                _context.SaveChanges();
                return true;
            }
            return false;
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

        public void EditShoppingCartItem(UpdateShoppingCartItemDto shoppingCartItemDto)
        {
            var CartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemDto);
            if (CartItem != null)
            {
                _context.ShoppingCartItems.Update(CartItem);
                _context.SaveChanges();
            }

        }
        public DetailedShoppingCartItemDto GetShoppingCartItem(int id)
        {
            return _mapper.ProjectTo<DetailedShoppingCartItemDto>(_context.ShoppingCartItems).FirstOrDefault(x => x.Id == id);
            //var cart = _context.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
            //return _mapper.Map<ShoppingCartItemDto>(cart);
        }
        public List<DetailedShoppingCartItemDto> GetShoppingCartItems()
        {
            // var CartItems = _context.ShoppingCartItems.ToList();
            // return _mapper.Map<List<ShoppingCartItemDto>>(CartItems);

            return _mapper.ProjectTo<DetailedShoppingCartItemDto>(_context.ShoppingCartItems).ToList();
        }

    }
}
