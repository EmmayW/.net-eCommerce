using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedGearMart.Models.DomainModel
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string? FirstName
        {
            get; set;
        }

        [Required]
        public string? LastName
        {
            get; set;
        }

        public string? Address { get; set; }

        public ICollection<Order> Activities { get; set; } = new List<Order>();

        public ICollection<Cart> carts { get; set; } = new List<Cart>();
    }
}
