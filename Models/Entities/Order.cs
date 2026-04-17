using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BepNha.Web.Models.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string OrderCode { get; set; } = string.Empty; // DH20260001

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? DeliveryAddress { get; set; }

        public DateTime PickupTime { get; set; }
        public int? TimeSlotId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0;

        [MaxLength(500)]
        public string? Note { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "pending";
        // pending | confirmed | cooking | cooked | shipping | done | cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? ConfirmedBy { get; set; }

        // Navigation
        public TimeSlot? TimeSlot { get; set; }
        public User? ConfirmedByUser { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<OrderStatusLog> StatusLogs { get; set; } = new List<OrderStatusLog>();
    }
}
