using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.Entities
{
    public class Table
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string TableCode { get; set; } = string.Empty; // BAN_01

        public int Capacity { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "available"; // available | reserved | occupied

        [MaxLength(200)]
        public string? Note { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<TableBooking> TableBookings { get; set; } = new List<TableBooking>();
    }
}
