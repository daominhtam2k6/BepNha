using BepNha.Web.Models.Entities;

namespace BepNha.Web.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        Task<(List<MenuItem> items, int total)> GetPagedAsync(int page, int pageSize, string search = null);
        Task<List<MenuItem>> GetAllAvailableAsync();
        Task<List<string>> GetCategoriesAsync();
        Task<List<MenuItem>> GetByIdsAsync(List<int> ids);
        Task<MenuItem> CreateAsync(MenuItem item);
        Task UpdateAsync(MenuItem item);
        Task DeleteAsync(int id);
    }
}
