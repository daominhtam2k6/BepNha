using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _repo;
        public TimeSlotService(ITimeSlotRepository repo) => _repo = repo;

        public async Task<List<TimeSlotDto>> GetAllAsync()
        {
            var slots = await _repo.GetAllAsync();
            return slots.Select(Map).ToList();
        }

        public async Task<List<TimeSlotDto>> GetActiveAsync()
        {
            var slots = await _repo.GetActiveAsync();
            return slots.Select(Map).ToList();
        }

        public async Task<ServiceResult> SaveAsync(TimeSlotDto dto)
        {
            if (dto.Id.HasValue && dto.Id > 0)
            {
                var slot = await _repo.GetByIdAsync(dto.Id.Value);
                if (slot == null) return ServiceResult.Fail("Không tìm thấy khung giờ.");
                slot.SlotName    = dto.SlotName;
                slot.StartTime   = dto.StartTime;
                slot.EndTime     = dto.EndTime;
                slot.MaxCapacity = dto.MaxCapacity;
                slot.IsActive    = dto.IsActive;
                await _repo.UpdateAsync(slot);
            }
            else
            {
                await _repo.CreateAsync(new TimeSlot
                {
                    SlotName    = dto.SlotName,
                    StartTime   = dto.StartTime,
                    EndTime     = dto.EndTime,
                    MaxCapacity = dto.MaxCapacity,
                    IsActive    = dto.IsActive
                });
            }
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return ServiceResult.Ok();
        }

        private static TimeSlotDto Map(TimeSlot s) => new()
        {
            Id          = s.Id,
            SlotName    = s.SlotName,
            StartTime   = s.StartTime,
            EndTime     = s.EndTime,
            MaxCapacity = s.MaxCapacity,
            IsActive    = s.IsActive
        };
    }
}
