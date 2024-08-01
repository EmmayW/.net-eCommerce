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
using MedGearMart.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> Index(string? category, string? search, string sortOrder="", int page = 1, int pageSize = 9)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            var gears = from g in _context.Gears.Include(g => g.Category) select g;

            if (!string.IsNullOrEmpty(search))
            {
                gears = gears.Where(g => g.GearName.Contains(search));
            }
            // Apply category filter
            if (!string.IsNullOrEmpty(category))
            {
                if (int.TryParse(category, out int categoryId))
                {
                    gears = gears.Where(g => g.Category.CategoryId == categoryId);
                }
            }
           
            

            switch (sortOrder)
            {
                case "name_desc":
                    gears = gears.OrderByDescending(g => g.GearName);
                    break;
                case "Price":
                    gears = gears.OrderBy(g => g.Price);
                    break;
                case "price_desc":
                    gears = gears.OrderByDescending(g => g.Price);
                    break;
                default:
                    gears = gears.OrderBy(g => g.GearName);
                    break;
            }

            var paginatedList = await PaginatedList<Gear>.CreateAsync(gears.AsNoTracking(), page, pageSize);

            //ViewBag.Categories = new SelectList(_context.Categories, "CategoryName", "CategoryName");
            // Retrieve categories for the filter dropdown
            var categories = await _context.Categories
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                .ToListAsync();
            ViewBag.Categories = categories;
            return View(paginatedList);
        }


        // GET: Gears/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if ( _context.Gears == null)
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
