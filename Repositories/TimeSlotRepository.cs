using Microsoft.EntityFrameworkCore;
using BepNha.Web.Data;
using BepNha.Web.Models.Entities;
using BepNha.Web.Repositories.Interfaces;

namespace BepNha.Web.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly AppDbContext _db;
        public TimeSlotRepository(AppDbContext db) => _db = db;

        public async Task<List<TimeSlot>> GetAllAsync()
            => await _db.TimeSlots.OrderBy(t => t.StartTime).ToListAsync();

        public async Task<List<TimeSlot>> GetActiveAsync()
            => await _db.TimeSlots.Where(t => t.IsActive).OrderBy(t => t.StartTime).ToListAsync();

        public async Task<TimeSlot?> GetByIdAsync(int id) => await _db.TimeSlots.FindAsync(id);

        public async Task<TimeSlot> CreateAsync(TimeSlot slot)
        {
            _db.TimeSlots.Add(slot);
            await _db.SaveChangesAsync();
            return slot;
        }

        public async Task UpdateAsync(TimeSlot slot)
        {
            _db.TimeSlots.Update(slot);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slot = await _db.TimeSlots.FindAsync(id);
            if (slot != null) { slot.IsActive = false; await _db.SaveChangesAsync(); }
        }
    }
}
