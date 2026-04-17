using BepNha.Web.Models.Entities;

namespace BepNha.Web.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<TableBooking>> GetPendingAsync();
        Task<List<TableBooking>> GetByPhoneAsync(string phone);
        Task<List<TableBooking>> GetByDateAsync(DateOnly date);
        Task<TableBooking?> GetByIdAsync(int id);
        Task<TableBooking> CreateAsync(TableBooking booking);
        Task UpdateStatusAsync(int id, string newStatus, int? changedBy, int? tableId, string? note);
        Task<int> CountConfirmedBySlotAsync(int timeSlotId, DateOnly date);
    }
}
