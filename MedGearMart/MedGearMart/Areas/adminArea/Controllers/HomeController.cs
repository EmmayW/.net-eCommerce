using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using MedGearMart.Models.DataLayer;
using MedGearMart.Models.DomainModel;
using MedGearMart.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace adminArea.Controllers
{
    [Area("adminArea")]
    public class HomeController : Controller
    {
        private readonly MedGearMartDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const int PageSize = 10;

        public HomeController(MedGearMartDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();

            var monthlySalesData = orders
                .GroupBy(o => o.OrderTime.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(o => o.OrderTotal),
                    TotalOrders = g.Count(),
                    AverageOrderValue = g.Sum(o => o.OrderTotal) / g.Count()
                })
                .OrderBy(g => g.Month)
                .ToList();

            var viewModel = new OverviewViewModel
            {
                TotalOrders = orders.Count(),
                TotalRevenue = orders.Sum(o => o.OrderTotal),
                MonthlySales = monthlySalesData.Select(m => (int)m.TotalSales).ToList(),
                MonthlyOrders = monthlySalesData.Select(m => m.TotalOrders).ToList(),
                MonthlyRevenue = monthlySalesData.Select(m => m.TotalSales).ToList(),
                AverageOrderValue = monthlySalesData.Select(m => m.AverageOrderValue).ToList(),
                Months = monthlySalesData.Select(m => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Month)).ToList()
            };

            return View(viewModel);
        }

        public IActionResult PendingOrders()
        {
            var pendingOrders = _context.Orders
                .Where(o => o.OrderStatus == "Pending" || o.OrderStatus == "Processing")
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList();

            return View(pendingOrders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);

            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.SaveChanges();
            }

            return RedirectToAction("PendingOrders");
        }

        public IActionResult AddGear()
        {
            var viewModel = new GearViewModel
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddGear(GearViewModel model, IFormFile imageFile)
        {
           
            string imagePath = null;

            // Handle file upload
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "imgs");
                string fileName = Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                imagePath = fileName;
            }

            var newGear = new Gear
            {
                GearName = model.GearName,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = imagePath,
                CategoryId = model.CategoryId
            };

            _context.Gears.Add(newGear);
            _context.SaveChanges();

            return RedirectToAction("Index");
          
        }

        // Action to list products with search and pagination
        public IActionResult UpdateStockList(string searchString, int pageNumber = 1)
        {
            var products = _context.Gears.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(g => g.GearName.Contains(searchString));
            }

            // Pagination
            var paginatedProducts = products
                .OrderBy(g => g.GearName)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var totalProducts = products.Count();
            var viewModel = new UpdateStockListViewModel
            {
                Products = paginatedProducts,
                CurrentPage = pageNumber,
                TotalPages = (int)System.Math.Ceiling(totalProducts / (double)PageSize),
                SearchString = searchString
            };

            return View(viewModel);
        }

        // Action to handle the stock update
        [HttpPost]
        public IActionResult UpdateStock(int gearId, int newStock)
        {
            var gear = _context.Gears.Find(gearId);
            if (gear == null)
            {
                return NotFound();
            }

            gear.Stock = newStock;
            _context.SaveChanges();

            return RedirectToAction("UpdateStockList");
        }

    }
}
