using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;

namespace BepNha.Web.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResult<BookingVm>> CreateBookingAsync(CreateBookingDto dto);
        Task<ServiceResult> ConfirmAsync(int id, int? tableId, int adminId);
        Task<ServiceResult> CancelAsync(int id, int adminId);
        Task<ServiceResult> CompleteAsync(int id, int adminId);
        Task<List<BookingVm>> GetPendingAsync();
        Task<List<BookingVm>> GetByDateAsync(DateOnly date);
        Task<List<BookingVm>> SearchByPhoneAsync(string phone);
    }
}
