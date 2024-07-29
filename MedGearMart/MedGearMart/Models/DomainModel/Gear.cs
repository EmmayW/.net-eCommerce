using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedGearMart.Models.DomainModel
{
    public class Gear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearId { get; set; }

        public string GearName { get; set;}
        public string? Description { get; set;}

        public decimal Price { get; set;}

        public int Stock { get;set;}

        public string imageUrl { get; set;}

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
