using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Controllers
{
    [Authorize(Roles = "Staff,Admin")]
    public class StaffController : Controller
    {
        private readonly IOrderService _orderService;
        public StaffController(IOrderService orderService) => _orderService = orderService;

        // GET /Staff
        public IActionResult Index() => View();

        // ── TAB BẾP: Đơn đang chế biến ──
        [HttpGet]
        public async Task<IActionResult> GetKitchenOrders()
            => Json(await _orderService.GetForKitchenAsync());

        // ── TAB BÀN GIAO: Đơn đã nấu xong chờ shipper ──
        [HttpGet]
        public async Task<IActionResult> GetCookedOrders()
            => Json(await _orderService.GetCookedAsync());

        // ── TAB ĐANG GIAO: Đơn đã bàn giao shipper ──
        [HttpGet]
        public async Task<IActionResult> GetShippingOrders()
            => Json(await _orderService.GetShippingAsync());

        // ── Cập nhật trạng thái bếp (cooking / cooked) ──
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string action)
        {
            var result = await _orderService.UpdateKitchenStatusAsync(id, action, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        // ── Cập nhật từng món ──
        [HttpPost]
        public async Task<IActionResult> UpdateItemStatus(int itemId, string status)
        {
            var result = await _orderService.UpdateItemStatusAsync(itemId, status, GetUserId());
            return Json(new { success = result.IsSuccess });
        }

        // ── Bàn giao đơn cho shipper ──
        [HttpPost]
        public async Task<IActionResult> HandoverToShipper(int id, string shipperName, string shipperPhone)
        {
            if (string.IsNullOrWhiteSpace(shipperName))
                return Json(new { success = false, message = "Vui lòng nhập tên shipper." });
            if (string.IsNullOrWhiteSpace(shipperPhone))
                return Json(new { success = false, message = "Vui lòng nhập SĐT shipper." });

            var result = await _orderService.HandoverToShipperAsync(id, shipperName.Trim(), shipperPhone.Trim(), GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        // ── Xác nhận giao hàng thành công ──
        [HttpPost]
        public async Task<IActionResult> ConfirmDelivered(int id)
        {
            var result = await _orderService.UpdateKitchenStatusAsync(id, "done", GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        private int GetUserId()
            => int.Parse(User.FindFirst("UserId")?.Value ?? "0");
    }
}
