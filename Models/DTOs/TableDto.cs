using System.ComponentModel.DataAnnotations;

namespace BepNha.Web.Models.DTOs
{
    public class TableDto
    {
        public int? Id { get; set; }

        [Required]
        public string TableCode { get; set; } = string.Empty;

        [Range(1, 20)]
        public int Capacity { get; set; }

        public string? Note { get; set; }
    }

    public class TimeSlotDto
    {
        public int? Id { get; set; }

        [Required]
        public string SlotName { get; set; } = string.Empty;

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int MaxCapacity { get; set; } = 10;
        public bool IsActive { get; set; } = true;
    }
}
