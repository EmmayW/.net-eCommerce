using Microsoft.AspNetCore.Mvc;
using MedGearMart.Models.DataLayer;
using MedGearMart.Models.ViewModels;
using System.Linq;
using MedGearMart.Models.DomainModel;
using MedGearMart.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedGearMart.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MedGearMartDbContext _context;
        private const string CartKey = "mycart";
        private const string CartCount = "mycount";

        public OrdersController(MedGearMartDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PlaceOrder(string FirstName, string LastName, string Email, string Address, string Address2, string Country, string Province, string Zip, string PaymentMethod, string CCName, string CCNumber, string CCExpiration, string CCCVV)
        {
            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Carts");
            }

            // Create a new Order object
            var order = new Order
            {
                UserId = userId,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Address = Address,
                Address2 = Address2,
                Country = Country,
                Province = Province,
                Zip = Zip,
                PaymentMethod = PaymentMethod,
                OrderTotal = cart.Sum(item => item.Product.Price * item.Quantity),
                OrderTime = DateTime.Now,  // Set the order time
                OrderStatus = "Pending"   // Set a default order status
            };

            // Save the order to the database first
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Now that the order is saved and has an OrderId, save the OrderItems
            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,  // Use the generated OrderId
                    ProductId = item.Product.GearId,
                    Quantity = item.Quantity,
                };
                _context.OrderItems.Add(orderItem);
            }

            // Save the order items to the database
            _context.SaveChanges();

            // Clear the cart session
            HttpContext.Session.Remove(CartKey);
            HttpContext.Session.Remove(CartCount);

            // Redirect to an Order Confirmation page or similar
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _context.Orders.Include(o => o.OrderItems)
                                       .ThenInclude(oi => oi.Product)
                                       .FirstOrDefault(o => o.OrderId == orderId);
            return View(order);
        }

        public IActionResult OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _context.Orders
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderTime)
                                 .ToList();

            return View(orders);
        }

        public IActionResult OrderHistoryDetail(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Step 1: Retrieve the Order
            var order = _context.Orders
                                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Step 2: Retrieve the related OrderItems
            var orderItems = _context.OrderItems
                                     .Include(oi => oi.Product)
                                     .Where(oi => oi.OrderId == orderId)
                                     .ToList();

            // Step 3: Attach the OrderItems to the Order
            order.OrderItems = orderItems;

            // Step 4: Return the View with the Order
            return View(order);
        }



    }
}
