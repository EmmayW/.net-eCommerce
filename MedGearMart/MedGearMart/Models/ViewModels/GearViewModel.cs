using MedGearMart.Models.DomainModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedGearMart.Models.ViewModels
{
    public class GearViewModel
    {
        public int GearId { get; set; }

        [Required]
        public string GearName { get; set; }

        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
