using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _repo;
        public TableService(ITableRepository repo) => _repo = repo;

        public async Task<List<TableVm>> GetAllAsync()
        {
            var tables = await _repo.GetAllAsync();
            return tables.Select(t => new TableVm
            {
                Id        = t.Id,
                TableCode = t.TableCode,
                Capacity  = t.Capacity,
                Status    = t.Status,
                Note      = t.Note
            }).ToList();
        }

        public async Task<ServiceResult> CreateAsync(TableDto dto)
        {
            var code = dto.TableCode.Trim().ToUpper();

            // 🔥 Check tồn tại (kể cả đã xóa)
            var existed = await _repo.GetByCodeAsync(code);

            if (existed != null)
            {
                if (!existed.IsActive)
                {
                    // 👉 Khôi phục lại bàn cũ
                    existed.IsActive = true;
                    existed.Capacity = dto.Capacity;
                    existed.Note = dto.Note;
                    existed.Status = "available";

                    await _repo.UpdateAsync(existed);
                    return ServiceResult.Ok("Khôi phục bàn thành công.");
                }

                return ServiceResult.Fail("Mã bàn đã tồn tại!");
            }

            var table = new Table
            {
                TableCode = code,
                Capacity = dto.Capacity,
                Note = dto.Note,
                Status = "available",
                IsActive = true
            };

            await _repo.CreateAsync(table);
            return ServiceResult.Ok("Thêm bàn thành công.");
        }

        public async Task<ServiceResult> UpdateAsync(int id, TableDto dto)
        {
            var table = await _repo.GetByIdAsync(id);
            if (table == null) return ServiceResult.Fail("Không tìm thấy bàn.");
            table.TableCode = dto.TableCode.Trim().ToUpper();
            table.Capacity  = dto.Capacity;
            table.Note      = dto.Note;
            await _repo.UpdateAsync(table);
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateStatusAsync(int id, string status, string? note)
        {
            await _repo.UpdateStatusAsync(id, status, note);
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return ServiceResult.Ok();
        }
    }
}
