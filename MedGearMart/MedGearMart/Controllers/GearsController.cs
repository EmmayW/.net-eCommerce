using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedGearMart.Models.DataLayer;
using MedGearMart.Models.DomainModel;
using MedGearMart.Models.Utils;

namespace MedGearMart.Controllers
{
    public class GearsController : Controller
    {
        private readonly MedGearMartDbContext _context;

        public GearsController(MedGearMartDbContext context)
        {
            _context = context;
        }

        // GET: Gears
        public async Task<IActionResult> Index(string? search, string? category)
        {
            // Retrieve categories for the filter dropdown
            var categories = await _context.Categories
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                .ToListAsync();
            ViewBag.Categories = categories;

            // Create the base query
            IQueryable<Gear> query = _context.Gears.Include(g => g.Category);

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(g => g.GearName.Contains(search) );
            }

            // Apply category filter
            if (!string.IsNullOrEmpty(category))
            {
                if (int.TryParse(category, out int categoryId))
                {
                    query = query.Where(g => g.CategoryId == categoryId);
                }
            }

            // Retrieve the filtered list of products
            var products = await query.ToListAsync();

            return View(products);


        }

        // GET: Gears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gears == null)
            {
                return NotFound();
            }

            var gear = await _context.Gears
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.GearId == id);
            if (gear == null)
            {
                return NotFound();
            }

            return View(gear);
        }

       
        private bool GearExists(int id)
        {
          return (_context.Gears?.Any(e => e.GearId == id)).GetValueOrDefault();
        }
    }
}
