using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.Entities
{
    public class TableBooking
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string BookingCode { get; set; } = string.Empty; // BK20260001

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;

        public DateOnly BookingDate { get; set; }

        public int TimeSlotId { get; set; }
        public int? TableId { get; set; }

        public int GuestCount { get; set; } = 1;

        [MaxLength(500)]
        public string? Note { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "pending";
        // pending | confirmed | cancelled | completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? ConfirmedBy { get; set; }

        // Navigation
        public TimeSlot TimeSlot { get; set; } = null!;
        public Table? Table { get; set; }
        public User? ConfirmedByUser { get; set; }
        public ICollection<BookingStatusLog> StatusLogs { get; set; } = new List<BookingStatusLog>();
    }
}
