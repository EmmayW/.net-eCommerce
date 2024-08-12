using Microsoft.AspNetCore.Mvc;
using MedGearMart.Models.DataLayer;
using MedGearMart.Models.Utils;
using MedGearMart.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MedGearMart.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly MedGearMartDbContext _context;
        private
        const string CartKey = "mycart";
        private
        const string CartCount = "mycount";

        public CartsController(MedGearMartDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();

            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            // Retrieve the gear details for the selected product
            var product = await _context.Gears.FindAsync(id);

            if (product == null)
            {
                // Product not found, return an error or redirect
                return NotFound();
            }

            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();

            // Find the item in the cart
            var existingItem = cart.FirstOrDefault(item => item.Product.GearId == id);

            if (existingItem != null)
            {
                // Item exists in the cart, increment the quantity
                existingItem.Quantity += 1;
            }
            else
            {
                // Item doesn't exist, add it with the specified quantity
                cart.Add(new CartViewModel
                {
                    Product = product,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObject(CartCount, cart.Sum(item => item.Quantity));

            // Save the cart to session
            HttpContext.Session.SetObject(CartKey, cart);

            // Optionally, redirect to a cart page or show a confirmation
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart([FromBody] CartUpdateModel updateModel)
        {
            int id = updateModel.Id;
            int quantity = updateModel.Quantity;

            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();

            var existingItem = cart.FirstOrDefault(c => c.Product.GearId == id);
            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
                // Ensure quantity is positive
                if (existingItem.Quantity <= 0)
                {
                    cart.Remove(existingItem);
                }
            }
            HttpContext.Session.SetObject(CartCount, cart.Sum(item => item.Quantity));

            // Save updated cart back to session
            HttpContext.Session.SetObject(CartKey, cart);

            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {

            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();

            var itemToRemove = cart.FirstOrDefault(c => c.Product.GearId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
            HttpContext.Session.SetObject(CartCount, cart.Sum(item => item.Quantity));

            // Save updated cart back to session
            HttpContext.Session.SetObject(CartKey, cart);

            return Ok();
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObject<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();
            return View("CheckoutPage", cart);
        }

    }

    public class CartUpdateModel
    {
        public int Id
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
    }

}