using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.Services;
using ProjectEcommerce.Data;

namespace ProjectEcommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cart = ShoppingCart.GetCart( HttpContext.RequestServices );
            var items = await cart.GetItems();
            return View( items );
        }

        public async Task<IActionResult> AddToCart( int id )
        {
            var product = await _context.Products.FindAsync( id );
            if (product != null)
            {
                var cart = ShoppingCart.GetCart( HttpContext.RequestServices );
                await cart.AddToCart( product );
            }

            return RedirectToAction( "Index", "Store" );
        }

        public async Task<IActionResult> RemoveFromCart( int id )
        {
            var cart = ShoppingCart.GetCart( HttpContext.RequestServices );
            await cart.RemoveFromCart( id );
            return RedirectToAction( "Index" );
        }
    }
}
