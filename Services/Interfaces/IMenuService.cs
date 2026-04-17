using BepNha.Web.Models.ViewModels;

namespace BepNha.Web.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuPagedVm> GetMenuPagedAsync(int page, int pageSize, string search = null);
        Task<List<MenuItemVm>> GetAllAvailableAsync();
        Task<ServiceResult> CreateAsync(MenuItemVm vm);
        Task<ServiceResult> UpdateAsync(int id, MenuItemVm vm);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
