using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedGearMart.Models.DomainModel
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        public string UserId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Gear Product {  get; set; }

        public int Quantity {get; set; }

        public decimal Subtotal => Product.Price * Quantity;

        
    }
}
