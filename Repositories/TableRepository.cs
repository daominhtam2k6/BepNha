using Microsoft.EntityFrameworkCore;
using BepNha.Web.Data;
using BepNha.Web.Models.Entities;
using BepNha.Web.Repositories.Interfaces;

namespace BepNha.Web.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _db;
        public TableRepository(AppDbContext db) => _db = db;

        public async Task<List<Table>> GetAllAsync()
            => await _db.Tables.Where(t => t.IsActive).OrderBy(t => t.TableCode).ToListAsync();

        public async Task<Table?> GetByIdAsync(int id) => await _db.Tables.FindAsync(id);

        public async Task<Table> CreateAsync(Table table)
        {
            _db.Tables.Add(table);
            await _db.SaveChangesAsync();
            return table;
        }
        public async Task<Table?> GetByCodeAsync(string code)
        {
            return await _db.Tables.FirstOrDefaultAsync(t => t.TableCode == code);
        }

        public async Task UpdateAsync(Table table)
        {
            _db.Tables.Update(table);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, string status, string? note)
        {
            var table = await _db.Tables.FindAsync(id);
            if (table == null) return;
            table.Status = status;
            if (note != null) table.Note = note;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = await _db.Tables.FindAsync(id);
            if (table != null) { table.IsActive = false; await _db.SaveChangesAsync(); }
        }
    }
}
