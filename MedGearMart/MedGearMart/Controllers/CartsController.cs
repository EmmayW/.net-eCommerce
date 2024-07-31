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
using Microsoft.CodeAnalysis;

namespace MedGearMart.Controllers
{
    public class CartsController : Controller
    {
        private readonly MedGearMartDbContext _context;

        public CartsController(MedGearMartDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var medGearMartDbContext = _context.Carts.Include(c => c.Product).Include(c => c.User);
            return View(await medGearMartDbContext.ToListAsync());
        }



        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId,ProductId,Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Gears, "GearId", "GearId", cart.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cart.UserId);
            return View(cart);
        }



        public async Task<IActionResult> AddToCart(int id)
        {
            // Retrieve the current user from the session
            var sessionUser = HttpContext.Session.GetObject<AppUser>("sessionUser");
            if (sessionUser == null)
            {
                return Unauthorized(); // Handle case where user is not logged in
            }

            // Create a unique session key to store the user's cart item
            string sessionCartUserId = "userCart-" + sessionUser.Id;

            // Retrieve the cart item from the session
            var sessionItemInCart = HttpContext.Session.GetObject<Cart>(sessionCartUserId);

            // Check if the cart item exists in the database
            var itemInCart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == sessionUser.Id && c.ProductId == id);

            if (itemInCart == null)
            {
                // If the item is not in the database, create a new cart item
                itemInCart = new Cart
                {
                    UserId = sessionUser.Id,
                    ProductId = id,
                    Quantity = 1
                };
                await _context.Carts.AddAsync(itemInCart);
            }
            else
            {
                // If the item exists, update the quantity
                itemInCart.Quantity += 1;
                _context.Carts.Update(itemInCart);
            }

            // Save the updated cart item to the session
            HttpContext.Session.SetObject(sessionCartUserId, itemInCart);

            // Save changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Return the view or redirect as needed
            return View(itemInCart);
        }


        public async Task<IActionResult> DeleteFromCart(int id)
        {
            // Retrieve the current user from the session
            var sessionUser = HttpContext.Session.GetObject<AppUser>("sessionUser");
            if (sessionUser == null)
            {
                return Unauthorized(); // Handle case where user is not logged in
            }

            // Create a unique session key to store the user's cart item
            string sessionCartUserId = "userCart-" + sessionUser.Id;

            // Retrieve the cart item from the database
            var itemInCart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == sessionUser.Id && c.ProductId == id);

            if (itemInCart != null)
            {
                // If the item is found, remove it from the database
                _context.Carts.Remove(itemInCart);
                await _context.SaveChangesAsync(); // Save changes asynchronously

                // Optionally, you can remove or update the session
                HttpContext.Session.Remove(sessionCartUserId); // Remove the item from the session
                                                               // If you have multiple items in the cart and want to keep others, 
                                                               // update the session accordingly instead of removing it entirely.
            }

            // Redirect or return a response as needed
            return RedirectToAction("Index"); // Or another appropriate action/view
        }


        public async Task<IActionResult> UpdateCart(int productId, int quantityChange)
        {
            // Retrieve the current user from the session
            var sessionUser = HttpContext.Session.GetObject<AppUser>("sessionUser");
            if (sessionUser == null)
            {
                return Unauthorized(); // Handle case where user is not logged in
            }

            // Find the cart item for the current user and product
            var itemInCart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == sessionUser.Id && c.ProductId == productId);

            if (itemInCart != null)
            {
                // Update the quantity
                itemInCart.Quantity += quantityChange;

                // Ensure the quantity does not go below zero
                if (itemInCart.Quantity <= 0)
                {
                    // Remove the item if quantity is zero or less
                    _context.Carts.Remove(itemInCart);
                }
                else
                {
                    // Update the item in the database
                    _context.Carts.Update(itemInCart);
                }

                // Save changes to the database asynchronously
                await _context.SaveChangesAsync();

                // Optionally, update the session
                string sessionCartUserId = "userCart-" + sessionUser.Id;
                HttpContext.Session.SetObject(sessionCartUserId, itemInCart);
            }

            // Redirect or return a response as needed
            return RedirectToAction("Index"); // Or another appropriate action/view
        }


        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
