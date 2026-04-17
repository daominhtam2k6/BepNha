using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuItemImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRwN9aL-rWiE84uhQtFyiGpEQuuzuFr4-5eN1EtrkwsIuqONeqYfnTkUTrcQAY1ykWJLTure1CWATyo9lijssqG4d7co0-XhGE2gHUuIE&s=10");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT5WYtKbS5Wlhs3rqXiWFxQ-uNQclEFsIK-0dGvaSi-QQISpMcVH6blrc4YHTTbrJxPbcx09HLYvQdcT6MTEQ8pDLCMCPAjpfhoLdG4xg&s=10");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTf1O9yeFBadtoTsTJOyWoO5MwsJQtA-Tey3JKHa1DR-hbBdw3slSfSPa6jSjIW3j2GILtXt0IugS4DFkIDx7S6ObmkcSzdLmin1_iTw&s=10");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://lh3.googleusercontent.com/gps-cs-s/AHVAwepQeDdO_6TRDmHskInYem6d7DYmSqTMyFkY-tUA8LD6zR_3C5eQ15yCxUicNBeVVy9pIEuTJRp6Wcx8BJm5OxXNztW9MUE1mRvSzSajlbtT1AWxc3MPDpVsZ6bmvXR3qZUgGNfoDg=w260-h175-n-k-no");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://vietnhatplastic.com/Data/Sites/1/News/294/cach-lam-nem-cuon-4.jpg");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://tse3.mm.bing.net/th/id/OIP.NbE2a1R238JYyH0bb1nfXQHaHa?rs=1&pid=ImgDetMain&o=7&rm=3");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.57096e43c834470d570dde5833ae5572?rik=7fqpfOL7A9rdGg&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://tse3.mm.bing.net/th/id/OIP.b6kfLQ84fnq5Kc-q4V2d1QHaEZ?rs=1&pid=ImgDetMain&o=7&rm=3");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$UIf5w.zWPabKnsIpcdYPr.YU2NpOXQmfuocgu/Wkk9913vyhR0UN6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$3bu0ZHMtD/Gy0I3bcCohKOQiHKn0iBRrvzhC/Z80YhwWBWRnLtYiy");
        }
    }
}
