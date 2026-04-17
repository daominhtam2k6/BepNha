using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Models.Entities;

namespace BepNha.Web.Services.Interfaces
{
    public interface ITableService
    {
        Task<List<TableVm>> GetAllAsync();
        Task<ServiceResult> CreateAsync(TableDto dto);
        Task<ServiceResult> UpdateAsync(int id, TableDto dto);
        Task<ServiceResult> UpdateStatusAsync(int id, string status, string? note);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
