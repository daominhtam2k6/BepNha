using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.Entities
{
    public class BookingStatusLog
    {
        public int Id { get; set; }
        public int BookingId { get; set; }

        [MaxLength(20)]
        public string? OldStatus { get; set; }

        [Required, MaxLength(20)]
        public string NewStatus { get; set; } = string.Empty;

        public int? ChangedBy { get; set; }

        [MaxLength(300)]
        public string? Note { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public TableBooking Booking { get; set; } = null!;
        public User? ChangedByUser { get; set; }
    }
}
