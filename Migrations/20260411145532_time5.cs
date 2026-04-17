using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class time5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$QSMX3iha1X40kFBm.94rveDgEHqCFbLA0ltQ/jhwcNG9SLSkY0K2e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$1okvnP2EgHEK7j8gVu58jOAHZt53E11aymAWSo6zOrFk4Z.1cdAxG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
