namespace BepNha.Web.Models.ViewModels
{
    public class DailyReportVm
    {
        public DateOnly Date { get; set; }
        public int TotalOrders { get; set; }
        public int DoneOrders { get; set; }
        public int CancelOrders { get; set; }
        public int PendingOrders { get; set; }
        public decimal Revenue { get; set; }
        public List<TopItemVm> TopItems { get; set; } = new();
        public List<OrderSummaryVm> Orders { get; set; } = new();
    }

    public class TopItemVm
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }
}
