using BepNha.Web.Models.Entities;

namespace BepNha.Web.Repositories.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<List<TimeSlot>> GetAllAsync();
        Task<List<TimeSlot>> GetActiveAsync();
        Task<TimeSlot?> GetByIdAsync(int id);
        Task<TimeSlot> CreateAsync(TimeSlot slot);
        Task UpdateAsync(TimeSlot slot);
        Task DeleteAsync(int id);
    }
}
