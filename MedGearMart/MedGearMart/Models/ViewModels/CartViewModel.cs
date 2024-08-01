

using MedGearMart.Models.DomainModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace MedGearMart.Models.ViewModels
{
    public class CartViewModel
    {


        private const string CartKey = "mycart";
        private const string CountKey = "mycount";
        public Gear Product { get; set; }

        public int Quantity { get; set; }
        private ISession session { get; set; }

       
            public CartViewModel(HttpContext httpContext)
            {
            session = httpContext.Session;
            }


        

       /* public List<CartViewModel> GetCart()
        {
            var cart = session.Get<List<CartViewModel>>(CartKey) ?? new List<CartViewModel>();
            return cart;
        }

        public int GetCartCount()
        {
            var cart = GetCart();
            return cart.Sum(item => item.Quantity);
        }*/


    }
}
