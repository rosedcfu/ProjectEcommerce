using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;
using ProjectEcommerce.Models.ViewModels;

namespace ProjectEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _um;

        public HomeController( ILogger<HomeController> logger, 
        ApplicationDbContext ctx,
        UserManager<ApplicationUser> um)
        {
            _logger = logger;
            _ctx = ctx;
            _um = um;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
