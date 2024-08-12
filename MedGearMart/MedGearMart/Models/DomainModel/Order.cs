using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedGearMart.Models.DomainModel
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [DataType(DataType.Currency)]
        public decimal OrderTotal { get; set; } // Renamed from Amount

        public DateTime OrderTime { get; set; } = DateTime.Now; // Initializes to current time

        public string OrderStatus { get; set; } = "Pending"; // Default status

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // New properties added to capture billing and payment details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; } // Optional
        public string Country { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public string PaymentMethod { get; set; }
    }
}
