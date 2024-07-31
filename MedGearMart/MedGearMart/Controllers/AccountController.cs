using MedGearMart.Models.DomainModel;
using MedGearMart.Models.Utils;
using MedGearMart.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedGearMart.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        

        // Constructor.
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
           // _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Login
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    // Retrieve the user details from the database
                    var loggedUser = await userManager.FindByNameAsync(model.Username);
                    HttpContext.Session.SetObject("sessionUser", loggedUser);
                    ModelState.AddModelError("", "Success");
                    return Redirect("../Home/Index");
                }
                ModelState.AddModelError("", "Unable to login");
                return View();
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                };
                 
                var result = await userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Redirect("../Home/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("Login");
        }
    }
}
