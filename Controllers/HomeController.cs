using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceShop.Data;
using RiceShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RiceShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home page - show categories and featured products
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        // Product list by category
        public async Task<IActionResult> Products(int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null) return NotFound();

            return View(category);
        }

        // Product detail page
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
