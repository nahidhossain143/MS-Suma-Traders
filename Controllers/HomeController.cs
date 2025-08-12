using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceShop.Data;

namespace RiceShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        // GET: /
        public async Task<IActionResult> Index()
        {
            // show top 6 products as featured
            var featured = await _db.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Take(6)
                .ToListAsync();

            return View(featured);
        }

        public IActionResult Privacy() => View();
    }
}
