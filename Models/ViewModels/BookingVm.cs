namespace BepNha.Web.Models.ViewModels
{
    public class BookingVm
    {
        public int Id { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public DateOnly BookingDate { get; set; }
        public string TimeSlotName { get; set; } = string.Empty;
        public string? TableCode { get; set; }
        public int GuestCount { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
