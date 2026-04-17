using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;

namespace BepNha.Web.Services.Interfaces
{
    public interface ITimeSlotService
    {
        Task<List<TimeSlotDto>> GetAllAsync();
        Task<List<TimeSlotDto>> GetActiveAsync();
        Task<ServiceResult> SaveAsync(TimeSlotDto dto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
