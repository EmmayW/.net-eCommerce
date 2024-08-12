using MedGearMart.Models.DomainModel;
using System.Collections.Generic;

namespace MedGearMart.Models.ViewModels
{
    public class UpdateStockListViewModel
    {
        public List<Gear> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchString { get; set; }
    }
}
