using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BepNha.Web.Migrations
{
    /// <inheritdoc />
    public partial class time6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$I3Dqc3HLIoTLbCybeKKuG.9FgVSUWFgBmyQctyteJsqLevNoDK8IK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$LiSSecQb1pD3eZhm1S56AuszSqNxnU1HoYdJeSHaH.S70CYMvF91m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
