using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class kjflsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IluMistMhfEaEuEKe1Sz0exsYOLJh55mO6NQWqkf8cKWAzN3Ss.l6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$SY.Z7lA3kB.JmMJr/Y1X8eyK8AD3wIpjVIxf6Xkox7loTrxjj2btu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
