using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        public string? DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thời gian nhận món")]
        public DateTime PickupTime { get; set; }

        public int? TimeSlotId { get; set; }
        public string? Note { get; set; }

        [Required, MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất 1 món")]
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; } = 1;
        public string? Note { get; set; }
    }
}
