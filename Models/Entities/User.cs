using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Role { get; set; } = "Customer"; // Admin | Staff | Customer

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Order> ConfirmedOrders { get; set; } = new List<Order>();
        public ICollection<TableBooking> ConfirmedBookings { get; set; } = new List<TableBooking>();
        public ICollection<OrderStatusLog> OrderStatusLogs { get; set; } = new List<OrderStatusLog>();
        public ICollection<BookingStatusLog> BookingStatusLogs { get; set; } = new List<BookingStatusLog>();
    }
}
