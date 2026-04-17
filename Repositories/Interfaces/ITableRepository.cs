using BepNha.Web.Models.Entities;

namespace BepNha.Web.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task<List<Table>> GetAllAsync();
        Task<Table?> GetByIdAsync(int id);
        Task<Table> CreateAsync(Table table);
        Task UpdateAsync(Table table);
        Task UpdateStatusAsync(int id, string status, string? note);
        Task DeleteAsync(int id);
        Task<Table?> GetByCodeAsync(string code);
    }
}
