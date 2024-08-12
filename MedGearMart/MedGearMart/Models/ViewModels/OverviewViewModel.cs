namespace MedGearMart.Models.ViewModels
{
    public class OverviewViewModel
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<int> MonthlySales { get; set; } = new List<int>();
        public List<int> MonthlyOrders { get; set; } = new List<int>();
        public List<decimal> MonthlyRevenue { get; set; } = new List<decimal>();
        public List<decimal> AverageOrderValue { get; set; } = new List<decimal>();
        public List<string> Months { get; set; } = new List<string>();
    }
}
