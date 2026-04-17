namespace BepNha.Web.Models.ViewModels
{
    public class OrderVm
    {
        public List<OrderSummaryVm> Orders { get; set; } = new();
    }

    public class OrderSummaryVm
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? DeliveryAddress { get; set; }
        public DateTime PickupTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemVm> Items { get; set; } = new();

        // Thông tin shipper - được điền khi đơn ở trạng thái shipping
        public string? ShipperName { get; set; }
        public string? ShipperPhone { get; set; }
    }

    public class OrderItemVm
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}