using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMenuRepository _menuRepo;

        public OrderService(IOrderRepository repo, IMenuRepository menuRepo)
        {
            _repo = repo;
            _menuRepo = menuRepo;
        }

        private static readonly Dictionary<string, string> StatusActionMap = new()
        {
            { "confirm", "confirmed" },
            { "cooking", "cooking" },
            { "cooked", "cooked" },
            { "ship", "shipping" },
            { "done", "done" },
            { "cancel", "cancelled" }
        };

        public async Task<ServiceResult<OrderSummaryVm>> CreateOrderAsync(CreateOrderDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
                return ServiceResult<OrderSummaryVm>.Fail("Đơn hàng phải có ít nhất 1 món.");

            // 🔥 Lấy danh sách id từ request
            var menuIds = dto.Items.Select(i => i.MenuItemId).ToList();

            // 🔥 Lấy menu thật từ DB
            var menuItems = await _menuRepo.GetByIdsAsync(menuIds);

            if (menuItems.Count != menuIds.Count)
                return ServiceResult<OrderSummaryVm>.Fail("Có món ăn không hợp lệ.");

            var order = new Order
            {
                OrderCode = GenerateCode("DH"),
                CustomerName = dto.CustomerName.Trim(),
                CustomerPhone = dto.CustomerPhone.Trim(),
                DeliveryAddress = dto.DeliveryAddress,
                PickupTime = dto.PickupTime,
                TimeSlotId = dto.TimeSlotId,
                Note = dto.Note,
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var menu = menuItems.First(m => m.Id == item.MenuItemId);

                order.Items.Add(new OrderItem
                {
                    MenuItemId = menu.Id,
                    Quantity = item.Quantity,
                    UnitPrice = menu.Price, // 🔥 FIX: lấy từ DB
                    Note = item.Note,
                    Status = "pending"
                });
            }

            order.TotalPrice = order.Items.Sum(i => i.UnitPrice * i.Quantity);

            var created = await _repo.CreateAsync(order);
            return ServiceResult<OrderSummaryVm>.Ok(MapToVm(created));
        }

        public async Task<ServiceResult> ConfirmOrderAsync(int id, int adminId)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return ServiceResult.Fail("Không tìm thấy đơn hàng.");
            if (order.Status != "pending") return ServiceResult.Fail("Đơn hàng không ở trạng thái chờ xác nhận.");
            await _repo.UpdateStatusAsync(id, "confirmed", adminId, "Admin xác nhận đơn");
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> CancelOrderAsync(int id, int adminId)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return ServiceResult.Fail("Không tìm thấy đơn hàng.");
            if (order.Status is "done" or "cancelled") return ServiceResult.Fail("Không thể hủy đơn này.");
            await _repo.UpdateStatusAsync(id, "cancelled", adminId, "Admin hủy đơn");
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateKitchenStatusAsync(int id, string action, int staffId)
        {
            if (!StatusActionMap.TryGetValue(action, out var newStatus))
                return ServiceResult.Fail("Hành động không hợp lệ.");
            await _repo.UpdateStatusAsync(id, newStatus, staffId, $"Cập nhật: {action}");
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateItemStatusAsync(int itemId, string status, int staffId)
        {
            await _repo.UpdateItemStatusAsync(itemId, status);
            return ServiceResult.Ok();
        }

        public async Task<List<OrderSummaryVm>> GetActiveOrdersAsync()
        {
            var orders = await _repo.GetActiveAsync();
            return orders.Select(MapToVm).ToList();
        }

        public async Task<List<OrderSummaryVm>> GetForKitchenAsync()
        {
            var orders = await _repo.GetForKitchenAsync();
            return orders.Select(MapToVm).ToList();
        }

        public async Task<List<OrderSummaryVm>> GetCookedAsync()
        {
            var orders = await _repo.GetCookedAsync();
            return orders.Select(MapToVm).ToList();
        }

        public async Task<List<OrderSummaryVm>> GetShippingAsync()
        {
            var orders = await _repo.GetShippingAsync();
            return orders.Select(MapToVm).ToList();
        }

        public async Task<ServiceResult> HandoverToShipperAsync(int id, string shipperName, string shipperPhone, int staffId)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return ServiceResult.Fail("Không tìm thấy đơn hàng.");
            if (order.Status != "cooked") return ServiceResult.Fail("Đơn hàng chưa sẵn sàng để bàn giao.");

            // ── FIX: Lưu thông tin shipper vào Order.Note với prefix đặc biệt
            //        để MapToVm có thể đọc và trả về frontend ──
            var shipperTag = $"[SHIPPER:{shipperName}|{shipperPhone}]";
            // Giữ lại ghi chú gốc (nếu có), ghép thêm shipper tag
            var existingNote = order.Note ?? string.Empty;
            // Xóa shipper tag cũ nếu đã có (đề phòng gọi lại)
            var cleanNote = System.Text.RegularExpressions.Regex.Replace(existingNote, @"\[SHIPPER:[^\]]*\]", "").Trim();
            order.Note = string.IsNullOrEmpty(cleanNote) ? shipperTag : $"{cleanNote} {shipperTag}";

            var logNote = $"Bàn giao cho shipper: {shipperName} -- SĐT: {shipperPhone}";
            await _repo.UpdateStatusAsync(id, "shipping", staffId, logNote);

            // Cập nhật Note trực tiếp trên order entity (UpdateStatusAsync không lưu Note)
            // Cần gọi thêm để lưu order.Note
            await _repo.UpdateNoteAsync(id, order.Note);

            return ServiceResult.Ok($"Đã bàn giao cho {shipperName}");
        }

        public async Task<List<OrderSummaryVm>> SearchByPhoneAsync(string phone)
        {
            var orders = await _repo.GetByPhoneAsync(phone);
            return orders.Select(MapToVm).ToList();
        }

        public async Task<DailyReportVm> GetDailyReportAsync(DateOnly date)
        {
            var orders = await _repo.GetByDateAsync(date);
            var done = orders.Where(o => o.Status == "done").ToList();

            return new DailyReportVm
            {
                Date = date,
                TotalOrders = orders.Count,
                DoneOrders = done.Count,
                CancelOrders = orders.Count(o => o.Status == "cancelled"),
                PendingOrders = orders.Count(o => o.Status != "done" && o.Status != "cancelled"),
                Revenue = done.Sum(o => o.TotalPrice),
                Orders = orders.Select(MapToVm).ToList(),
                TopItems = done
                    .SelectMany(o => o.Items)
                    .GroupBy(i => i.MenuItem?.Name ?? "Không xác định")
                    .Select(g => new TopItemVm
                    {
                        Name = g.Key,
                        Quantity = g.Sum(i => i.Quantity),
                        Revenue = g.Sum(i => i.UnitPrice * i.Quantity)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .Take(10)
                    .ToList()
            };
        }

        public async Task<TodayStatsVm> GetTodayStatsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var orders = await _repo.GetByDateAsync(today);
            return new TodayStatsVm
            {
                NewOrders = orders.Count(o => o.Status == "pending"),
                DoneOrders = orders.Count(o => o.Status == "done"),
                CancelOrders = orders.Count(o => o.Status == "cancelled"),
                Revenue = orders.Where(o => o.Status == "done").Sum(o => o.TotalPrice)
            };
        }

        // ── Helpers ──
        private static string GenerateCode(string prefix)
            => $"{prefix}{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(10, 99)}";

        public static string GetStatusText(string status) => status switch
        {
            "pending" => "Chờ xác nhận",
            "confirmed" => "Đã xác nhận",
            "cooking" => "Đang chế biến",
            "cooked" => "Chờ giao",
            "shipping" => "Đang giao",
            "done" => "Hoàn tất",
            "cancelled" => "Đã hủy",
            _ => status
        };

        private static OrderSummaryVm MapToVm(Order o)
        {
            // Parse thông tin shipper từ Order.Note nếu đơn đang ở trạng thái shipping
            string? shipperName = null;
            string? shipperPhone = null;
            string? cleanNote = o.Note;

            if (!string.IsNullOrEmpty(o.Note))
            {
                var match = System.Text.RegularExpressions.Regex.Match(o.Note, @"\[SHIPPER:(.+?)\|(.+?)\]");
                if (match.Success)
                {
                    shipperName = match.Groups[1].Value;
                    shipperPhone = match.Groups[2].Value;
                    // Trả về note sạch (không có shipper tag) cho frontend
                    cleanNote = System.Text.RegularExpressions.Regex.Replace(o.Note, @"\[SHIPPER:[^\]]*\]", "").Trim();
                    if (string.IsNullOrEmpty(cleanNote)) cleanNote = null;
                }
            }

            return new OrderSummaryVm
            {
                Id = o.Id,
                OrderCode = o.OrderCode,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                DeliveryAddress = o.DeliveryAddress,
                PickupTime = o.PickupTime,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                StatusText = GetStatusText(o.Status),
                Note = cleanNote,
                CreatedAt = o.CreatedAt,
                ShipperName = shipperName,
                ShipperPhone = shipperPhone,
                Items = o.Items.Select(i => new OrderItemVm
                {
                    Id = i.Id,
                    MenuItemName = i.MenuItem?.Name ?? "",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Note = i.Note,
                    Status = i.Status
                }).ToList()
            };
        }
    }
}