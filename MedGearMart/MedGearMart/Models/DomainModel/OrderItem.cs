using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        public int Quantity { get; set; }

        public decimal Subtotal => Product.Price * Quantity;

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        


    }
}
