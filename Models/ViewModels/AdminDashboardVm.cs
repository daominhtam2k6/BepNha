namespace BepNha.Web.Models.ViewModels
{
    public class AdminDashboardVm
    {
        public List<TableVm> Tables { get; set; } = new();
        public TodayStatsVm TodayStats { get; set; } = new();
    }

    public class TableVm
    {
        public int Id { get; set; }
        public string TableCode { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = "available";
        public string? Note { get; set; }
    }

    public class TodayStatsVm
    {
        public int NewOrders { get; set; }
        public int DoneOrders { get; set; }
        public int CancelOrders { get; set; }
        public decimal Revenue { get; set; }
        public int OccupiedTables { get; set; }
        public int TotalTables { get; set; }
    }

    public class MenuItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
