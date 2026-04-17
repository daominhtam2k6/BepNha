using Microsoft.EntityFrameworkCore;
using BepNha.Web.Data;
using BepNha.Web.Models.Entities;
using BepNha.Web.Repositories.Interfaces;

namespace BepNha.Web.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _db;
        public BookingRepository(AppDbContext db) => _db = db;

        private IQueryable<TableBooking> WithIncludes()
            => _db.TableBookings
                .Include(b => b.TimeSlot)
                .Include(b => b.Table)
                .Include(b => b.ConfirmedByUser);

        public async Task<List<TableBooking>> GetPendingAsync()
            => await WithIncludes()
                .Where(b => b.Status == "pending")
                .OrderBy(b => b.BookingDate).ThenBy(b => b.TimeSlot.StartTime)
                .ToListAsync();

        public async Task<List<TableBooking>> GetByPhoneAsync(string phone)
            => await WithIncludes()
                .Where(b => b.CustomerPhone == phone)
                .OrderByDescending(b => b.CreatedAt).ToListAsync();

        public async Task<List<TableBooking>> GetByDateAsync(DateOnly date)
            => await WithIncludes()
                .Where(b => b.BookingDate == date)
                .OrderBy(b => b.TimeSlot.StartTime).ToListAsync();

        public async Task<TableBooking?> GetByIdAsync(int id)
            => await WithIncludes().FirstOrDefaultAsync(b => b.Id == id);

        public async Task<TableBooking> CreateAsync(TableBooking booking)
        {
            _db.TableBookings.Add(booking);
            _db.BookingStatusLogs.Add(new BookingStatusLog
            {
                Booking   = booking,
                OldStatus = null,
                NewStatus = booking.Status,
                ChangedAt = DateTime.UtcNow,
                Note      = "Khách hàng tạo đặt bàn"
            });
            await _db.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateStatusAsync(int id, string newStatus, int? changedBy, int? tableId, string? note)
        {
            var booking = await _db.TableBookings.FindAsync(id);
            if (booking == null) return;
            var log = new BookingStatusLog
            {
                BookingId = id,
                OldStatus = booking.Status,
                NewStatus = newStatus,
                ChangedBy = changedBy,
                Note      = note,
                ChangedAt = DateTime.UtcNow
            };
            booking.Status    = newStatus;
            booking.UpdatedAt = DateTime.UtcNow;
            if (tableId.HasValue) booking.TableId = tableId;
            if (changedBy.HasValue) booking.ConfirmedBy = changedBy;
            _db.BookingStatusLogs.Add(log);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CountConfirmedBySlotAsync(int timeSlotId, DateOnly date)
            => await _db.TableBookings
                .Where(b => b.TimeSlotId == timeSlotId && b.BookingDate == date
                         && (b.Status == "pending" || b.Status == "confirmed"))
                .SumAsync(b => b.GuestCount);
    }
}
