using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BepNha.Web.Models.DTOs;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        private readonly IBookingService _bookingService;
        private readonly ITimeSlotService _slotService;

        public HomeController(IMenuService menuService, IOrderService orderService,
                              IBookingService bookingService, ITimeSlotService slotService)
        {
            _menuService = menuService;
            _orderService = orderService;
            _bookingService = bookingService;
            _slotService = slotService;
        }

        // GET / – Xem thực đơn (công khai)
        [AllowAnonymous]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var vm = await _menuService.GetMenuPagedAsync(page, 8, search);
            return View(vm);
        }

        // GET /Home/GetMenuItems
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
            => Json(await _menuService.GetAllAvailableAsync());

        // GET /Home/GetTimeSlots – Khung giờ cho form đặt bàn
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTimeSlots()
            => Json(await _slotService.GetActiveAsync());

        // POST /Home/CreateOrder – UC03: Đặt món mang về (KHÔNG cần đăng nhập)
        [AllowAnonymous]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (dto == null)
                return Json(new { success = false, message = "Không nhận được dữ liệu." });

            if (dto.Items == null || dto.Items.Count == 0)
                return Json(new { success = false, message = "Vui lòng chọn ít nhất 1 món." });

            if (string.IsNullOrWhiteSpace(dto.CustomerName))
                return Json(new { success = false, message = "Vui lòng nhập tên khách hàng." });

            if (string.IsNullOrWhiteSpace(dto.CustomerPhone))
                return Json(new { success = false, message = "Vui lòng nhập số điện thoại." });

            if (string.IsNullOrWhiteSpace(dto.DeliveryAddress))
                return Json(new { success = false, message = "Vui lòng nhập địa chỉ giao hàng." });

            var result = await _orderService.CreateOrderAsync(dto);
            return Json(new
            {
                success = result.IsSuccess,
                message = result.Message,
                orderCode = result.Data?.OrderCode
            });
        }

        // POST /Home/CreateBooking – UC04: Đặt bàn (KHÔNG cần đăng nhập)
        [AllowAnonymous]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if (dto == null)
                return Json(new { success = false, message = "Không nhận được dữ liệu." });

            if (string.IsNullOrWhiteSpace(dto.CustomerName))
                return Json(new { success = false, message = "Vui lòng nhập tên khách hàng." });

            if (string.IsNullOrWhiteSpace(dto.CustomerPhone))
                return Json(new { success = false, message = "Vui lòng nhập số điện thoại." });

            if (dto.TimeSlotId <= 0)
                return Json(new { success = false, message = "Vui lòng chọn khung giờ." });

            if (dto.GuestCount <= 0)
                return Json(new { success = false, message = "Số người không hợp lệ." });

            var result = await _bookingService.CreateBookingAsync(dto);
            return Json(new
            {
                success = result.IsSuccess,
                message = result.Message,
                bookingCode = result.Data?.BookingCode
            });
        }

        // GET /Home/Search?phone=xxx – UC05: Tra cứu (KHÔNG cần đăng nhập)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Json(new { success = false, message = "Vui lòng nhập số điện thoại." });

            var orders = await _orderService.SearchByPhoneAsync(phone.Trim());
            var bookings = await _bookingService.SearchByPhoneAsync(phone.Trim());
            return Json(new { success = true, orders, bookings });
        }
    }
}
