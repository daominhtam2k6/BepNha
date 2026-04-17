using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.Entities;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _repo.GetByUsernameAsync(username.Trim().ToLower());
            if (user == null) return null;
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
        {
            var existing = await _repo.GetByUsernameAsync(dto.Username.Trim().ToLower());
            if (existing != null)
                return ServiceResult.Fail("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.");

            var user = new User
            {
                FullName = dto.FullName.Trim(),
                Username = dto.Username.Trim().ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.CreateAsync(user);
            return ServiceResult.Ok("Đăng ký thành công!");
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            var existing = await _repo.GetByUsernameAsync(username);
            return existing == null;
        }
    }
}
