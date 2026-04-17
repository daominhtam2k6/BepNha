using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.DTOs
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ngày đặt bàn")]
        public DateOnly BookingDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khung giờ")]
        public int TimeSlotId { get; set; }

        [Range(1, 50, ErrorMessage = "Số lượng người từ 1 đến 50")]
        public int GuestCount { get; set; } = 1;

        public string? Note { get; set; }
    }
}
