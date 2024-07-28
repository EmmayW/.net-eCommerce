using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedGearMart.Models
{
    public class AppUser: IdentityUser
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
    }
}
