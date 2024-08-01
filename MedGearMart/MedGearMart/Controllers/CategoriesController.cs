
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedGearMart.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;

namespace MedGearMart.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly MedGearMartDbContext _context;

        public CategoriesController(MedGearMartDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'MedGearMartDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

       
    }
}
