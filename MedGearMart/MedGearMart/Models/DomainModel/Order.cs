using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

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

        public decimal Amount { get; set; }

        public DateTime OrderTime { get; set; }

        public string OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();



    }
}
