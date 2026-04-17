using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly ITimeSlotRepository _slotRepo;

        public BookingService(IBookingRepository repo, ITimeSlotRepository slotRepo)
        {
            _repo     = repo;
            _slotRepo = slotRepo;
        }

        public async Task<ServiceResult<BookingVm>> CreateBookingAsync(CreateBookingDto dto)
        {
            // Kiểm tra slot có tồn tại
            var slot = await _slotRepo.GetByIdAsync(dto.TimeSlotId);
            if (slot == null || !slot.IsActive)
                return ServiceResult<BookingVm>.Fail("Khung giờ không hợp lệ.");

            // Kiểm tra sức chứa
            var booked = await _repo.CountConfirmedBySlotAsync(dto.TimeSlotId, dto.BookingDate);
            if (booked + dto.GuestCount > slot.MaxCapacity)
                return ServiceResult<BookingVm>.Fail("Khung giờ này đã đạt sức chứa tối đa. Vui lòng chọn giờ khác.");

            var booking = new TableBooking
            {
                BookingCode   = GenerateCode("BK"),
                CustomerName  = dto.CustomerName.Trim(),
                CustomerPhone = dto.CustomerPhone.Trim(),
                BookingDate   = dto.BookingDate,
                TimeSlotId    = dto.TimeSlotId,
                GuestCount    = dto.GuestCount,
                Note          = dto.Note,
                Status        = "pending"
            };

            var created = await _repo.CreateAsync(booking);
            // Reload với includes
            var full = await _repo.GetByIdAsync(created.Id);
            return ServiceResult<BookingVm>.Ok(MapToVm(full!));
        }

        public async Task<ServiceResult> ConfirmAsync(int id, int? tableId, int adminId)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return ServiceResult.Fail("Không tìm thấy đặt bàn.");
            if (booking.Status != "pending") return ServiceResult.Fail("Đặt bàn không ở trạng thái chờ.");
            await _repo.UpdateStatusAsync(id, "confirmed", adminId, tableId, "Admin xác nhận");
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> CancelAsync(int id, int adminId)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return ServiceResult.Fail("Không tìm thấy đặt bàn.");
            if (booking.Status is "completed" or "cancelled") return ServiceResult.Fail("Không thể hủy.");
            await _repo.UpdateStatusAsync(id, "cancelled", adminId, null, "Admin hủy đặt bàn");
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> CompleteAsync(int id, int adminId)
        {
            await _repo.UpdateStatusAsync(id, "completed", adminId, null, "Hoàn tất phục vụ");
            return ServiceResult.Ok();
        }

        public async Task<List<BookingVm>> GetPendingAsync()
        {
            var list = await _repo.GetPendingAsync();
            return list.Select(MapToVm).ToList();
        }

        public async Task<List<BookingVm>> GetByDateAsync(DateOnly date)
        {
            var list = await _repo.GetByDateAsync(date);
            return list.Select(MapToVm).ToList();
        }

        public async Task<List<BookingVm>> SearchByPhoneAsync(string phone)
        {
            var list = await _repo.GetByPhoneAsync(phone);
            return list.Select(MapToVm).ToList();
        }

        private static string GenerateCode(string prefix)
            => $"{prefix}{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(10, 99)}";

        public static string GetStatusText(string status) => status switch
        {
            "pending"   => "Chờ xác nhận",
            "confirmed" => "Đã xác nhận",
            "cancelled" => "Đã hủy",
            "completed" => "Hoàn tất",
            _ => status
        };

        private static BookingVm MapToVm(TableBooking b) => new()
        {
            Id            = b.Id,
            BookingCode   = b.BookingCode,
            CustomerName  = b.CustomerName,
            CustomerPhone = b.CustomerPhone,
            BookingDate   = b.BookingDate,
            TimeSlotName  = b.TimeSlot?.SlotName ?? "",
            TableCode     = b.Table?.TableCode,
            GuestCount    = b.GuestCount,
            Note          = b.Note,
            Status        = b.Status,
            StatusText    = GetStatusText(b.Status),
            CreatedAt     = b.CreatedAt
        };
    }
}
