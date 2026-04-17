using Microsoft.EntityFrameworkCore;
using BepNha.Web.Data;
using BepNha.Web.Models.Entities;
using BepNha.Web.Repositories.Interfaces;

namespace BepNha.Web.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _db;
        public MenuRepository(AppDbContext db) => _db = db;

        public async Task<(List<MenuItem>, int)> GetPagedAsync(int page, int pageSize, string search = null)
        {
            var query = _db.MenuItems
                .AsNoTracking()
                .Where(x => x.IsAvailable);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.SortOrder) // 🔥 thay vì Name
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<List<MenuItem>> GetAllAvailableAsync()
            => await _db.MenuItems
                .AsNoTracking()
                .Where(m => m.IsAvailable)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _db.MenuItems
                .Where(m => m.IsAvailable && !string.IsNullOrEmpty(m.Category))
                .Select(m => m.Category!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetByIdsAsync(List<int> ids)
        {
            return await _db.MenuItems
                    .Where(m => ids.Contains(m.Id) && m.IsAvailable)
                    .OrderBy(m => m.SortOrder)
                    .ToListAsync();
        }

        public async Task<MenuItem> CreateAsync(MenuItem item)
        {
            _db.MenuItems.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(MenuItem item)
        {
            _db.MenuItems.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _db.MenuItems.FindAsync(id);
            if (item != null)
            {
                item.IsAvailable = false;
                await _db.SaveChangesAsync();
            }
        }
    }
}
