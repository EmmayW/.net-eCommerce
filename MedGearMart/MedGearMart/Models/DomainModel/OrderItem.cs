using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedGearMart.Models.DomainModel
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Gear Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

    }
}
