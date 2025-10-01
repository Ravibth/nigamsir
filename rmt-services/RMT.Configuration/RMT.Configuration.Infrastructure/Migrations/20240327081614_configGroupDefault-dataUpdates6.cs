using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configGroupDefaultdataUpdates6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 64L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
               table: "ConfigurationGroupMasters",
               keyColumn: "Id",
               keyValue: 16L,
               columns: new[] { "ConfigKey", "CongigDisplayText" },
               values: new object[] { "Industry", "Industry" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "ConfigKey", "CongigDisplayText" },
                values: new object[] { "Industry", "Industry" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
               table: "ConfigurationGroupMasters",
               keyColumn: "Id",
               keyValue: 16L,
               columns: new[] { "ConfigKey", "CongigDisplayText" },
               values: new object[] { "Sector", "Sector" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "ConfigKey", "CongigDisplayText" },
                values: new object[] { "Sector", "Sector" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 64L,
                column: "IsActive",
                value: true);
        }
    }
}
