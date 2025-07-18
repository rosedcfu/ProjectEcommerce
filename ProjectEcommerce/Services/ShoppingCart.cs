using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;

namespace ProjectEcommerce.Services
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;

        private ShoppingCart(ApplicationDbContext context, string userId )
        {
            _context = context;
            _userId = userId;
        }

        public static ShoppingCart GetCart(IServiceProvider services )
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var userId = httpContext.User.Identity.IsAuthenticated
                ? httpContext.User.Identity.Name
                : httpContext.Session.GetString("CartId") ?? Guid.NewGuid().ToString();

            if (!httpContext.User.Identity.IsAuthenticated)
                httpContext.Session.SetString("CartId", userId);
            return new ShoppingCart( context, userId );
        }

        public async Task AddToCart( Product product )
        {
            var cartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync( c => c.ProductId == product.Id && c.UserId == _userId );

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ProductId = product.Id,
                    UserId = _userId,
                    Quantity = 1
                };

                _context.ShoppingCartItems.Add( cartItem );
            }
            else
            {
                cartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetItems()
        {
            return await _context.ShoppingCartItems.Include(i => i.Product).Where(i => i.UserId == _userId).ToListAsync();
        }

        public async Task RemoveFromCart(int productId )
        {
            var item = await _context.ShoppingCartItems.FirstOrDefaultAsync(i => i.ProductId == productId && i.UserId == _userId);

            if (item != null)
            {
                _context.ShoppingCartItems.Remove( item );
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearCart()
        {
            var items = _context.ShoppingCartItems.Where( i => i.UserId == _userId );
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
