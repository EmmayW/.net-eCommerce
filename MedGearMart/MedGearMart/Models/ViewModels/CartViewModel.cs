

using MedGearMart.Models.DomainModel;
using MedGearMart.Models.Utils;

namespace MedGearMart.Models.ViewModels
{
    public class CartViewModel
    {



        private const string CountKey = "mycount";
        public Gear Product { get; set; }

        public int Quantity { get; set; }
        private ISession session { get; set; }
        public CartViewModel()
        {

        }

        public CartViewModel(HttpContext httpContext)
        {
            session = httpContext.Session;
        }

        public int Count()
        {

            return session?.GetObject<int>(CountKey) ?? 0;
        }




    }
}
