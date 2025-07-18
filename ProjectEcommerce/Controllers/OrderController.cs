using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;
using ProjectEcommerce.Services;

namespace ProjectEcommerce.Controllers
{
    [Authorize] // Solo usuarios logueados pueden comprar
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController( ApplicationDbContext context )
        {
            _context = context;
        }
        
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.Identity.Name;

            var orders = await _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Confirm()
        {
            var cart = ShoppingCart.GetCart( HttpContext.RequestServices );
            var cartItems = await cart.GetItems();

            if (!cartItems.Any())
                return RedirectToAction( "Index", "Cart" );

            // Crear la orden
            var order = new Order
            {
                UserId = User.Identity.Name,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum( i => i.Product.Price * i.Quantity ),
                Items = cartItems.Select( i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                } ).ToList()
            };

            _context.Orders.Add( order );
            await _context.SaveChangesAsync();

            // Vaciar el carrito
            await cart.ClearCart();

            return View( "Confirmation" );
        }
        [Authorize( Roles = "Admin" )]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _context.Orders
                .Include( o => o.Items )
                    .ThenInclude( i => i.Product )
                .OrderByDescending( o => o.OrderDate )
                .ToListAsync();

            return View( orders );
        }


    }
}
