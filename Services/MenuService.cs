using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BepNha.Web.Services   
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;
        public MenuService(IMenuRepository repo) => _repo = repo;

        public async Task<MenuPagedVm> GetMenuPagedAsync(int page, int pageSize, string search = null)
        {
            var (items, total) = await _repo.GetPagedAsync(page, pageSize, search);
            var categories     = await _repo.GetCategoriesAsync();
            return new MenuPagedVm
            {
                Items       = items.Select(MapToVm).ToList(),
                CurrentPage = page,
                TotalPages  = (int)Math.Ceiling((double)total / pageSize),
                TotalItems  = total,
                Categories  = categories
            };
        }

        public async Task<List<MenuItemVm>> GetAllAvailableAsync()
        {
            var items = await _repo.GetAllAvailableAsync();
            return items.Select(MapToVm).ToList();
        }

        public async Task<ServiceResult> CreateAsync(MenuItemVm vm)
        {
            var item = new MenuItem
            {
                Name        = vm.Name,
                Description = vm.Description,
                Price       = vm.Price,
                ImageUrl    = vm.ImageUrl,
                Category    = vm.Category,
                IsAvailable = vm.IsAvailable
            };
            await _repo.CreateAsync(item);
            return ServiceResult.Ok("Thêm món thành công.");
        }

        public async Task<ServiceResult> UpdateAsync(int id, MenuItemVm vm)
        {
            // Lấy món theo id
            var item = (await _repo.GetByIdsAsync(new List<int> { id })).FirstOrDefault();

            if (item == null)
                return ServiceResult.Fail("Không tìm thấy món.");

            // Update dữ liệu
            item.Name = vm.Name;
            item.Description = vm.Description;
            item.Price = vm.Price;
            item.ImageUrl = vm.ImageUrl;
            item.Category = vm.Category;
            item.IsAvailable = vm.IsAvailable;

            await _repo.UpdateAsync(item);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return ServiceResult.Ok();
        }

        private static MenuItemVm MapToVm(MenuItem m) => new()
        {
            Id          = m.Id,
            Name        = m.Name,
            Description = m.Description,
            Price       = m.Price,
            ImageUrl    = m.ImageUrl,
            Category    = m.Category,
            IsAvailable = m.IsAvailable
        };
    }
}
