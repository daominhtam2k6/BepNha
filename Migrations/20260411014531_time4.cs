using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class time4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://cdn.tgdd.vn/2021/01/CookProduct/1200fgsd-1200x675.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://file.hstatic.net/200000700229/article/com-ga-chien-mam-toi-1_598c51ff37f84acd99f186d64e0acba0.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCZXv8SZ-pB6Qf686UrKMTXvt1-nLP8WSVzg&s");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://horecavn.com/wp-content/uploads/2024/05/bac-xiu-da_20240527105417.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://dayphache.edu.vn/wp-content/uploads/2024/06/cach-lam-tra-dao.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://product.hstatic.net/200000723779/product/e1b07ac46dcb4bdd835ec1e98cd4cf83_d1bc01a388704ddd8539a59a8dd6a7b2_grande.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://tmart.nanoweb.vn/public/uploads/all/kHrlDUmImj7pEVSr0QpAOGR4oMIOC7rWbaRvpMmx.png");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQie4Hw_GApMLyRn34hNhlXh46_33_56ZcfMA&s");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://congthucphache.com/wp-content/uploads/2019/03/CONG-THUC-PHA-CHE-CHANH-DAY-TUYET-PASSION-SNOW.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://www.unileverfoodsolutions.com.vn/dam/global-ufs/mcos/phvn/vietnam/calcmenu/recipes/VN-recipes/other/sweet-lychee-tea/main-header.jpg", "Trà Vải" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$AEZGVoiNtdKVnTxXeWk4b.cUFgVzhnX47O.fzayb9e6vc5Zqw35S6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$wSfFMGjaVyXYYF7U9tLX.eUcPtoBrP5gJWsmLwJ7Rk9HPPSads0yi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://statics.vinpearl.com/mi-quang-da-nang-1_1628755088.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://statics.vinpearl.com/com-ga-hoi-an-1_1628755088.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://caphenguyenchat.vn/wp-content/uploads/2023/06/ca-phe-sua-da.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://file.hstatic.net/1000075078/article/bac-xiu-la-gi_867809633e214d02b545d625514f08a4.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://nguyenlieulache.com/wp-content/uploads/2020/07/tra-dao-cam-sa.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://Product.hstatic.net/200000350701/product/nuoc_ep_cam_tuoi_9c8b8b8b8b8b8b8b8b8b8b8b8b8b8b8b_master.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://phuclong.com.vn/uploads/dish/0b7f6b9b6e5b4b-trasuatranchau.png");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://cdn.tgdd.vn/Files/2020/05/20/1256943/bi-quyet-lam-sinh-to-bo-thom-ngon-beo-ngay-ma-khong-bi-dang-202005201025537542.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://bartender.edu.vn/wp-content/uploads/2018/06/chanh-day-tuyet.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://i.ytimg.com/vi/3N_mC6tL7Xk/maxresdefault.jpg", "Trà Vải Khiếm Thân" });

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
    }
}
