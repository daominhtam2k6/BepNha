using Microsoft.EntityFrameworkCore;
using BepNha.Web.Data;
using BepNha.Web.Models.Entities;
using BepNha.Web.Repositories.Interfaces;

namespace BepNha.Web.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db) => _db = db;

        private IQueryable<Order> WithIncludes()
            => _db.Orders
                .Include(o => o.Items).ThenInclude(i => i.MenuItem)
                .Include(o => o.TimeSlot)
                .Include(o => o.ConfirmedByUser);

        public async Task<List<Order>> GetActiveAsync()
            => await WithIncludes()
                .Where(o => o.Status != "done" && o.Status != "cancelled")
                .OrderByDescending(o => o.CreatedAt).ToListAsync();

        public async Task<List<Order>> GetByPhoneAsync(string phone)
            => await WithIncludes()
                .Where(o => o.CustomerPhone == phone)
                .OrderByDescending(o => o.CreatedAt).ToListAsync();

        public async Task<List<Order>> GetByDateAsync(DateOnly date)
        {
            var start = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var end = date.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
            return await WithIncludes()
                .Where(o => o.CreatedAt >= start && o.CreatedAt <= end)
                .OrderByDescending(o => o.CreatedAt).ToListAsync();
        }

        public async Task<List<Order>> GetForKitchenAsync()
            => await WithIncludes()
                .Where(o => o.Status == "confirmed" || o.Status == "cooking")
                .OrderBy(o => o.PickupTime).ToListAsync();

        public async Task<List<Order>> GetCookedAsync()
            => await WithIncludes()
                .Where(o => o.Status == "cooked")
                .OrderBy(o => o.PickupTime).ToListAsync();

        public async Task<List<Order>> GetShippingAsync()
            => await WithIncludes()
                .Where(o => o.Status == "shipping")
                .OrderByDescending(o => o.UpdatedAt).ToListAsync();

        public async Task<Order?> GetByIdAsync(int id)
            => await WithIncludes().FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> CreateAsync(Order order)
        {
            _db.Orders.Add(order);
            _db.OrderStatusLogs.Add(new OrderStatusLog
            {
                Order = order,
                OldStatus = null,
                NewStatus = order.Status,
                ChangedAt = DateTime.UtcNow,
                Note = "Khách hàng tạo đơn"
            });
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task UpdateStatusAsync(int id, string newStatus, int? changedBy, string? note)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return;

            var log = new OrderStatusLog
            {
                OrderId = id,
                OldStatus = order.Status,
                NewStatus = newStatus,
                ChangedBy = changedBy,
                Note = note,
                ChangedAt = DateTime.UtcNow
            };

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            _db.OrderStatusLogs.Add(log);
            await _db.SaveChangesAsync();
        }

        /// <summary>Cập nhật ghi chú đơn hàng — dùng để lưu thông tin shipper</summary>
        public async Task UpdateNoteAsync(int id, string? note)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return;
            order.Note = note;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateItemStatusAsync(int itemId, string newStatus)
        {
            var item = await _db.OrderItems.FindAsync(itemId);
            if (item == null) return;
            item.Status = newStatus;
            await _db.SaveChangesAsync();
        }

        public async Task<List<Order>> GetTopItemsAsync(DateOnly from, DateOnly to)
        {
            var start = from.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var end = to.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
            return await WithIncludes()
                .Where(o => o.Status == "done" && o.CreatedAt >= start && o.CreatedAt <= end)
                .ToListAsync();
        }
    }
}