using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class display_hide_myapproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
             table: "MenuMaster",
             keyColumn: "Id",
             keyValue: 4L,
             columns: new[] { "IsDisplay"},
             values: new object[] { false});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
            table: "MenuMaster",
            keyColumn: "Id",
            keyValue: 4L,
            columns: new[] { "IsDisplay" },
            values: new object[] { true });
        }
    }
}
