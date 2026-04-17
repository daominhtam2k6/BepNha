using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;

namespace BepNha.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<ServiceResult> RegisterAsync(RegisterDto dto);
        Task<bool> IsUsernameAvailableAsync(string username);
    }
}
