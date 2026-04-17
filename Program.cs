using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using BepNha.Web.Data;
using BepNha.Web.Repositories;
using BepNha.Web.Repositories.Interfaces;
using BepNha.Web.Services;
using BepNha.Web.Services.Interfaces;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// ── Logging ──
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ── MVC ──
builder.Services.AddControllersWithViews();

// ── EF Core + SQLite (ENV-first) ──
var sqliteConnStr =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION")
    ?? $"Data Source={Environment.GetEnvironmentVariable("SQLITE_FILE") ?? "app.db"}";

Console.WriteLine($"[STARTUP] Using connection string: {sqliteConnStr}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(sqliteConnStr)
           .LogTo(Console.WriteLine, LogLevel.Information));

// ── Cookie Authentication ──
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

// ── Authorization ──
// Do NOT set a FallbackPolicy that requires authentication globally.
// By default endpoints are public; use [Authorize] on controllers/actions that require auth.
builder.Services.AddAuthorization();

// ── Repositories ──
// Correct mappings: interface -> repository implementation
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ── Services ──
// Correct mappings: interface -> service implementation
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// ── Auto migrate DB on startup ──
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("[STARTUP] Running EF Core migrations...");
        db.Database.Migrate();
        Console.WriteLine("[STARTUP] ✅ Migrations completed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[STARTUP] ❌ Migration failed: {ex.Message}");
        throw;
    }
}

// ── Middleware ──
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ── Port handling (Render) ──
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
Console.WriteLine($"[STARTUP] Starting app on http://0.0.0.0:{port}");
app.Run($"http://0.0.0.0:{int.Parse(port)}");