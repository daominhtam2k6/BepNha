using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using BepNha.Web.Models.DTOs;
using BepNha.Web.Models.ViewModels;
using BepNha.Web.Services.Interfaces;

namespace BepNha.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService) => _userService = userService;

        // ── LOGIN ──
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
                return View(dto);
            }

            await SignInUser(user.FullName, user.Role, user.Id, user.Username);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return user.Role switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Staff" => RedirectToAction("Index", "Staff"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ── REGISTER ──
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _userService.RegisterAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(dto);
            }

            // Đăng nhập tự động sau khi đăng ký
            var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
            if (user != null)
                await SignInUser(user.FullName, user.Role, user.Id, user.Username);

            TempData["SuccessMessage"] = $"🎉 Đăng ký thành công! Chào mừng {dto.FullName} đến với Bếp Nhà!";
            return RedirectToAction("Index", "Home");
        }

        // ── CHECK USERNAME (AJAX realtime) ──
        [HttpGet]
        public async Task<IActionResult> CheckUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
                return Json(new { available = false, message = "Tối thiểu 3 ký tự" });

            var available = await _userService.IsUsernameAvailableAsync(username.Trim().ToLower());
            return Json(new { available, message = available ? "Tên đăng nhập hợp lệ" : "Tên đăng nhập đã tồn tại" });
        }

        // ── LOGOUT ──
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();

        // ── HELPER ──
        private async Task SignInUser(string fullName, string role, int id, string username)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, fullName),
                new(ClaimTypes.Role, role),
                new("UserId",        id.ToString()),
                new("Username",      username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });
        }
    }
}
