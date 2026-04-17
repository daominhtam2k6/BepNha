using BepNha.Web.Models.Entities;

namespace BepNha.Web.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetActiveAsync();
        Task<List<Order>> GetByPhoneAsync(string phone);
        Task<List<Order>> GetByDateAsync(DateOnly date);
        Task<List<Order>> GetForKitchenAsync();
        Task<List<Order>> GetCookedAsync();      // Đơn chờ bàn giao shipper
        Task<List<Order>> GetShippingAsync();    // Đơn đang giao
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task UpdateStatusAsync(int id, string newStatus, int? changedBy, string? note);
        Task UpdateItemStatusAsync(int itemId, string newStatus);
        Task<List<Order>> GetTopItemsAsync(DateOnly from, DateOnly to);

        /// <summary>Cập nhật ghi chú đơn hàng (dùng để lưu thông tin shipper)</summary>
        Task UpdateNoteAsync(int id, string? note);
    }
}