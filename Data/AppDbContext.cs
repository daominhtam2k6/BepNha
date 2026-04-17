using Microsoft.EntityFrameworkCore;
using BepNha.Web.Models.Entities;

namespace BepNha.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableBooking> TableBookings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusLog> OrderStatusLogs { get; set; }
        public DbSet<BookingStatusLog> BookingStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Index tìm kiếm theo SĐT (UC05) ──
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerPhone);
            modelBuilder.Entity<TableBooking>()
                .HasIndex(b => b.CustomerPhone);

            // ── Unique code ──
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderCode).IsUnique();
            modelBuilder.Entity<TableBooking>()
                .HasIndex(b => b.BookingCode).IsUnique();
            modelBuilder.Entity<Table>()
                .HasIndex(t => t.TableCode).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            // ── Relationships ──
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ConfirmedByUser)
                .WithMany(u => u.ConfirmedOrders)
                .HasForeignKey(o => o.ConfirmedBy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TableBooking>()
                .HasOne(b => b.ConfirmedByUser)
                .WithMany(u => u.ConfirmedBookings)
                .HasForeignKey(b => b.ConfirmedBy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderStatusLog>()
                .HasOne(l => l.ChangedByUser)
                .WithMany(u => u.OrderStatusLogs)
                .HasForeignKey(l => l.ChangedBy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BookingStatusLog>()
                .HasOne(l => l.ChangedByUser)
                .WithMany(u => u.BookingStatusLogs)
                .HasForeignKey(l => l.ChangedBy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderStatusLog>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            // ── Seed: Admin mặc định ──
            modelBuilder.Entity<User>().HasData(new User
            {
                Id       = 1,
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                FullName = "Quản trị viên",
                Role     = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id       = 2,
                Username = "bep01",
                Password = BCrypt.Net.BCrypt.HashPassword("Staff@123"),
                FullName = "Nhân viên bếp",
                Role     = "Staff",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            // ── Seed: TimeSlots ──
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot { Id = 1, SlotName = "Sáng (7:00 - 10:00)",  StartTime = new TimeOnly(7, 0),  EndTime = new TimeOnly(10, 0), MaxCapacity = 20 },
                new TimeSlot { Id = 2, SlotName = "Trưa (11:00 - 14:00)", StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(14, 0), MaxCapacity = 30 },
                new TimeSlot { Id = 3, SlotName = "Chiều (14:00 - 17:00)",StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0), MaxCapacity = 20 },
                new TimeSlot { Id = 4, SlotName = "Tối (17:00 - 21:00)",  StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(21, 0), MaxCapacity = 30 }
            );

            // ── Seed: Tables ──
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, TableCode = "BAN_01", Capacity = 2, Status = "available" },
                new Table { Id = 2, TableCode = "BAN_02", Capacity = 4, Status = "available" },
                new Table { Id = 3, TableCode = "BAN_03", Capacity = 4, Status = "available" },
                new Table { Id = 4, TableCode = "BAN_04", Capacity = 6, Status = "available" },
                new Table { Id = 5, TableCode = "BAN_05", Capacity = 6, Status = "available" },
                new Table { Id = 6, TableCode = "BAN_06", Capacity = 8, Status = "available" }
            );

            // ── Seed: MenuItems ──
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "Phở Bò Gia Truyền",  Price = 60000, Category = "Món chính", SortOrder = 1, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRwN9aL-rWiE84uhQtFyiGpEQuuzuFr4-5eN1EtrkwsIuqONeqYfnTkUTrcQAY1ykWJLTure1CWATyo9lijssqG4d7co0-XhGE2gHUuIE&s=10" },
                new MenuItem { Id = 2, Name = "Bún Chả Hà Nội",     Price = 55000, Category = "Món chính", SortOrder = 2, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT5WYtKbS5Wlhs3rqXiWFxQ-uNQclEFsIK-0dGvaSi-QQISpMcVH6blrc4YHTTbrJxPbcx09HLYvQdcT6MTEQ8pDLCMCPAjpfhoLdG4xg&s=10" },
                new MenuItem { Id = 3, Name = "Bánh Mỳ Đặc Biệt",   Price = 35000, Category = "Món chính", SortOrder = 3, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTf1O9yeFBadtoTsTJOyWoO5MwsJQtA-Tey3JKHa1DR-hbBdw3slSfSPa6jSjIW3j2GILtXt0IugS4DFkIDx7S6ObmkcSzdLmin1_iTw&s=10" },
                new MenuItem { Id = 4, Name = "Cơm Tấm Sườn Bì",    Price = 65000, Category = "Món chính", SortOrder = 4, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://lh3.googleusercontent.com/gps-cs-s/AHVAwepQeDdO_6TRDmHskInYem6d7DYmSqTMyFkY-tUA8LD6zR_3C5eQ15yCxUicNBeVVy9pIEuTJRp6Wcx8BJm5OxXNztW9MUE1mRvSzSajlbtT1AWxc3MPDpVsZ6bmvXR3qZUgGNfoDg=w260-h175-n-k-no" },
                new MenuItem { Id = 5, Name = "Nem Cuốn Tươi",      Price = 40000, Category = "Món chính", SortOrder = 5, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://vietnhatplastic.com/Data/Sites/1/News/294/cach-lam-nem-cuon-4.jpg" },
                new MenuItem { Id = 6, Name = "Gỏi Cuốn Tôm Thịt",  Price = 45000, Category = "Món chính", SortOrder = 6, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://tse3.mm.bing.net/th/id/OIP.NbE2a1R238JYyH0bb1nfXQHaHa?rs=1&pid=ImgDetMain&o=7&rm=3" },
                new MenuItem { Id = 7, Name = "Bún Bò Huế",         Price = 60000, Category = "Món chính", SortOrder = 7, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://th.bing.com/th/id/R.57096e43c834470d570dde5833ae5572?rik=7fqpfOL7A9rdGg&pid=ImgRaw&r=0" },
                new MenuItem { Id = 8, Name = "Chả Giò Rán",        Price = 35000, Category = "Món chính", SortOrder = 8, IsAvailable = true, CreatedAt = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), ImageUrl = "https://tse3.mm.bing.net/th/id/OIP.b6kfLQ84fnq5Kc-q4V2d1QHaEZ?rs=1&pid=ImgDetMain&o=7&rm=3" },
                new MenuItem { Id = 9, Name = "Mì Quảng Tôm Thịt", Price = 55000, Category = "Món chính", SortOrder = 9, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://cdn.tgdd.vn/2021/01/CookProduct/1200fgsd-1200x675.jpg" },
                new MenuItem { Id = 10, Name = "Cơm Gà Hội An", Price = 65000, Category = "Món chính", SortOrder = 10, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://file.hstatic.net/200000700229/article/com-ga-chien-mam-toi-1_598c51ff37f84acd99f186d64e0acba0.jpg" },

                // Đồ uống
                new MenuItem { Id = 11, Name = "Cà Phê Sữa Đá", Price = 25000, Category = "Đồ uống", SortOrder = 11, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCZXv8SZ-pB6Qf686UrKMTXvt1-nLP8WSVzg&s" },
                new MenuItem { Id = 12, Name = "Bạc Xỉu", Price = 28000, Category = "Đồ uống", SortOrder = 12, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://horecavn.com/wp-content/uploads/2024/05/bac-xiu-da_20240527105417.jpg" },
                new MenuItem { Id = 13, Name = "Trà Đào Cam Sả", Price = 45000, Category = "Đồ uống", SortOrder = 13, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://dayphache.edu.vn/wp-content/uploads/2024/06/cach-lam-tra-dao.jpg" },
                new MenuItem { Id = 14, Name = "Nước Ép Cam Tươi", Price = 35000, Category = "Đồ uống", SortOrder = 14, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://product.hstatic.net/200000723779/product/e1b07ac46dcb4bdd835ec1e98cd4cf83_d1bc01a388704ddd8539a59a8dd6a7b2_grande.jpg" },
                new MenuItem { Id = 15, Name = "Trà Sữa Trân Châu", Price = 40000, Category = "Đồ uống", SortOrder = 15, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://tmart.nanoweb.vn/public/uploads/all/kHrlDUmImj7pEVSr0QpAOGR4oMIOC7rWbaRvpMmx.png" },
                new MenuItem { Id = 16, Name = "Sinh Tố Bơ", Price = 45000, Category = "Đồ uống", SortOrder = 16, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQie4Hw_GApMLyRn34hNhlXh46_33_56ZcfMA&s" },
                new MenuItem { Id = 17, Name = "Chanh Dây Tuyết", Price = 30000, Category = "Đồ uống", SortOrder = 17, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://congthucphache.com/wp-content/uploads/2019/03/CONG-THUC-PHA-CHE-CHANH-DAY-TUYET-PASSION-SNOW.jpg" },
                new MenuItem { Id = 18, Name = "Trà Vải", Price = 42000, Category = "Đồ uống", SortOrder = 18, IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), ImageUrl = "https://www.unileverfoodsolutions.com.vn/dam/global-ufs/mcos/phvn/vietnam/calcmenu/recipes/VN-recipes/other/sweet-lychee-tea/main-header.jpg" }
                        );
        }
    }
}
