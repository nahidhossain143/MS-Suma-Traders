using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceShop.Data;

namespace RiceShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        // GET: /Products
        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _db.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            var list = await query.OrderBy(p => p.Name).ToListAsync();

            ViewBag.Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.SelectedCategory = categoryId;

            return View(list);
        }

        // GET: /Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
