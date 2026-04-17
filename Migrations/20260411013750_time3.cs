using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class time3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "SortOrder" },
                values: new object[,]
                {
                    { 9, "Món chính", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://statics.vinpearl.com/mi-quang-da-nang-1_1628755088.jpg", true, "Mì Quảng Tôm Thịt", 55000m, 9 },
                    { 10, "Món chính", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://statics.vinpearl.com/com-ga-hoi-an-1_1628755088.jpg", true, "Cơm Gà Hội An", 65000m, 10 },
                    { 11, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://caphenguyenchat.vn/wp-content/uploads/2023/06/ca-phe-sua-da.jpg", true, "Cà Phê Sữa Đá", 25000m, 11 },
                    { 12, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://file.hstatic.net/1000075078/article/bac-xiu-la-gi_867809633e214d02b545d625514f08a4.jpg", true, "Bạc Xỉu", 28000m, 12 },
                    { 13, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://nguyenlieulache.com/wp-content/uploads/2020/07/tra-dao-cam-sa.jpg", true, "Trà Đào Cam Sả", 45000m, 13 },
                    { 14, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://Product.hstatic.net/200000350701/product/nuoc_ep_cam_tuoi_9c8b8b8b8b8b8b8b8b8b8b8b8b8b8b8b_master.jpg", true, "Nước Ép Cam Tươi", 35000m, 14 },
                    { 15, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://phuclong.com.vn/uploads/dish/0b7f6b9b6e5b4b-trasuatranchau.png", true, "Trà Sữa Trân Châu", 40000m, 15 },
                    { 16, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://cdn.tgdd.vn/Files/2020/05/20/1256943/bi-quyet-lam-sinh-to-bo-thom-ngon-beo-ngay-ma-khong-bi-dang-202005201025537542.jpg", true, "Sinh Tố Bơ", 45000m, 16 },
                    { 17, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://bartender.edu.vn/wp-content/uploads/2018/06/chanh-day-tuyet.jpg", true, "Chanh Dây Tuyết", 30000m, 17 },
                    { 18, "Đồ uống", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://i.ytimg.com/vi/3N_mC6tL7Xk/maxresdefault.jpg", true, "Trà Vải Khiếm Thân", 42000m, 18 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$VTnE1xk/qq8KOaftGKCZO.wVdMDN36Iyn0kAXkXK/zq20oBXuKxGa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$SP4JnUKRqXxbxlIOVGMaqOg2.F0FpOIM.sv2Pc2HdfzCQYxcEmgdW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$rhmnxR.oqo38pDsJwrZ2NetZtTylVEAOs//qWOIF0ioF92G/Tld8e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$9XbNMpbV75U6EW5GVuSRXetGTafKo5AFETvhpQWgguCEG1lAiYQQu");
        }
    }
}
