using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;

namespace BepNha.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult<OrderSummaryVm>> CreateOrderAsync(CreateOrderDto dto);
        Task<ServiceResult> ConfirmOrderAsync(int id, int adminId);
        Task<ServiceResult> CancelOrderAsync(int id, int adminId);
        Task<ServiceResult> UpdateKitchenStatusAsync(int id, string action, int staffId);
        Task<ServiceResult> UpdateItemStatusAsync(int itemId, string status, int staffId);
        Task<List<OrderSummaryVm>> GetActiveOrdersAsync();
        Task<List<OrderSummaryVm>> GetForKitchenAsync();
        Task<List<OrderSummaryVm>> GetCookedAsync();        // Chờ bàn giao
        Task<List<OrderSummaryVm>> GetShippingAsync();      // Đang giao
        Task<ServiceResult> HandoverToShipperAsync(int id, string shipperName, string shipperPhone, int staffId);
        Task<List<OrderSummaryVm>> SearchByPhoneAsync(string phone);
        Task<DailyReportVm> GetDailyReportAsync(DateOnly date);
        Task<TodayStatsVm> GetTodayStatsAsync();
    }
}
