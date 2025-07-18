using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;


namespace ProjectEcommerce.Controllers
{
    [Authorize( Roles = "Admin" )]
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Where( p => p.Stock > 0 )
                .ToListAsync();

            return View( products );
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product, IFormFile ImageFile )
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension( ImageFile.FileName );
                    var extension = Path.GetExtension( ImageFile.FileName );
                    var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                    var imagePath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/images", newFileName );

                    using (var stream = new FileStream( imagePath, FileMode.Create ))
                    {
                        await ImageFile.CopyToAsync( stream );
                    }

                    product.ImageUrl = $"/images/{newFileName}";
                }
                else
                {
                    // Imagen por defecto si no se sube ninguna
                    product.ImageUrl = "/images/no-image.png";
                }

                _context.Add( product );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }

            return View( product );
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ReduceStock = product.Stock
            };
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, Product model, IFormFile? ImageFile )
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View( model );

            var product = await _context.Products.FindAsync( id );
            if (product == null)
                return NotFound();

            // ✅ Actualizar propiedades básicas
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;

            // ✅ Si se sube una nueva imagen, reemplazarla
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension( ImageFile.FileName );
                var extension = Path.GetExtension( ImageFile.FileName );
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var imagePath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/images", newFileName );

                using (var stream = new FileStream( imagePath, FileMode.Create ))
                {
                    await ImageFile.CopyToAsync( stream );
                }

                product.ImageUrl = $"/images/{newFileName}";
            }
            // ✅ Si no se sube imagen nueva, se mantiene la actual

            await _context.SaveChangesAsync();
            TempData ["SuccessMessage"] = "✔️ Producto actualizado correctamente.";
            return RedirectToAction( nameof( Index ) );
        }
        //public async Task<IActionResult> Estadisticas()
        //{
        //    var top3 = await _context.OrderItems
        //        .GroupBy( i => i.Product )
        //        .Select( g => new {
        //            Product = g.Key,
        //            TotalVendidas = g.Sum( i => i.Quantity )
        //        } )
        //        .OrderByDescending( p => p.TotalVendidas )
        //        .Take( 3 )
        //        .ToListAsync();

        //    var totalVentas = await _context.Orders.SumAsync( o => o.TotalAmount );

        //    var productos = await _context.Products.ToListAsync();

        //    ViewData ["TotalVentas"] = totalVentas;
        //    ViewData ["Top3"] = top3;

        //    return View( productos );
        //}

        // POST : Products/UpdateStock
        [HttpPost]
        public async Task<IActionResult> UpdateStock( int id, int newStock )
        {
            var product = await _context.Products.FindAsync( id );
            if (product == null)
                return NotFound();

            if (newStock < 0)
            {
                ModelState.AddModelError( "", "❌ El stock no puede ser negativo." );
                return RedirectToAction( nameof( Index ) );
            }

            product.Stock = newStock;
            await _context.SaveChangesAsync();

            TempData ["SuccessMessage"] = "✔️ Stock actualizado correctamente.";
            return RedirectToAction( nameof( Index ) );
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/ReduceStock/5
        public async Task<IActionResult> ReduceStock( int? id )
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync( id );
            if (product == null) return NotFound();

            return View( product );
        }

        // POST: Product/ReduceStock/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReduceStock( int id, int quantityToRemove )
        {
            var product = await _context.Products.FindAsync( id );
            if (product == null)
                return NotFound();

            if (quantityToRemove <= 0 || quantityToRemove > product.Stock)
            {
                TempData ["ErrorMessage"] = "❌ No puedes eliminar más unidades de las disponibles.";
                return RedirectToAction( nameof( Index ) );
            }

            product.Stock -= quantityToRemove;
            await _context.SaveChangesAsync();

            TempData ["SuccessMessage"] = "✔️ Stock actualizado correctamente.";
            return RedirectToAction( nameof( Index ) );
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
