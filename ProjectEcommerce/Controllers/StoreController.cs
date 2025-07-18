using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.Data;

namespace ProjectEcommerce.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View( products );
        }
    }
}
