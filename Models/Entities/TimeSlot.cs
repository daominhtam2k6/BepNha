using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string SlotName { get; set; } = string.Empty;

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public int MaxCapacity { get; set; } = 10;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<TableBooking> TableBookings { get; set; } = new List<TableBooking>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
