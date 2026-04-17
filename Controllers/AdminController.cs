using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookingService _bookingService;
        private readonly ITableService _tableService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly IMenuService _menuService;

        public AdminController(IOrderService orderService, IBookingService bookingService,
                               ITableService tableService, ITimeSlotService timeSlotService,
                               IMenuService menuService)
        {
            _orderService = orderService;
            _bookingService = bookingService;
            _tableService = tableService;
            _timeSlotService = timeSlotService;
            _menuService = menuService;
        }

        // GET /Admin – Dashboard
        public async Task<IActionResult> Index()
        {
            var vm = new
            {
                Tables = await _tableService.GetAllAsync(),
                TodayStats = await _orderService.GetTodayStatsAsync()
            };
            return View(vm);
        }

        // ── UC06: Sơ đồ bàn ──
        [HttpGet]
        public async Task<IActionResult> GetTables()
            => Json(await _tableService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] TableDto dto)
        {
            var result = await _tableService.CreateAsync(dto);
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] TableDto dto)
        {
            var result = await _tableService.UpdateAsync(id, dto);
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTableStatus(int id, string status, string? note)
        {
            var result = await _tableService.UpdateStatusAsync(id, status, note);
            return Json(new { success = result.IsSuccess });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteAsync(id);
            return Json(new { success = result.IsSuccess });
        }

        // ── UC07: Đơn đặt bàn ──
        [HttpGet]
        public async Task<IActionResult> GetBookings()
            => Json(await _bookingService.GetPendingAsync());

        [HttpGet]
        public async Task<IActionResult> GetBookingsByDate(string date)
        {
            var d = DateOnly.Parse(date);
            return Json(await _bookingService.GetByDateAsync(d));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int id, int? tableId)
        {
            var result = await _bookingService.ConfirmAsync(id, tableId, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.CancelAsync(id, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        // ── UC07: Đơn gọi món ──
        [HttpGet]
        public async Task<IActionResult> GetOrders()
            => Json(await _orderService.GetActiveOrdersAsync());

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var result = await _orderService.ConfirmOrderAsync(id, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var result = await _orderService.CancelOrderAsync(id, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string action)
        {
            var result = await _orderService.UpdateKitchenStatusAsync(id, action, GetUserId());
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        // ── UC08: Khung giờ ──
        [HttpGet]
        public async Task<IActionResult> GetTimeSlots()
            => Json(await _timeSlotService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> SaveTimeSlot([FromBody] TimeSlotDto dto)
        {
            var result = await _timeSlotService.SaveAsync(dto);
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var result = await _timeSlotService.DeleteAsync(id);
            return Json(new { success = result.IsSuccess });
        }

        // ── UC09: Báo cáo ──
        [HttpGet]
        public async Task<IActionResult> GetReport(string date)
        {
            var d = DateOnly.Parse(date);
            var vm = await _orderService.GetDailyReportAsync(d);
            return Json(vm);
        }

        // ── Quản lý Menu ──
        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
            => Json(await _menuService.GetAllAvailableAsync());

        [HttpPost]
        public async Task<IActionResult> SaveMenuItem([FromBody] MenuItemVm dto)
        {
            ServiceResult result;
            if (dto.Id > 0)
                result = await _menuService.UpdateAsync(dto.Id, dto);
            else
                result = await _menuService.CreateAsync(dto);
            return Json(new { success = result.IsSuccess, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuService.DeleteAsync(id);
            return Json(new { success = result.IsSuccess });
        }

        private int GetUserId()
            => int.Parse(User.FindFirst("UserId")?.Value ?? "0");

    [HttpGet]
        public async Task<IActionResult> GetTodayStats()
        {
            var stats = await _orderService.GetTodayStatsAsync();
            return Json(stats);
        }
    }
}