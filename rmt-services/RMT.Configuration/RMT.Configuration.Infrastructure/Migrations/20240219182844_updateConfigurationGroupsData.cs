using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateConfigurationGroupsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 14);
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 15);
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 33);
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.UpdateData(
              table: "ConfigurationGroups",
              keyColumn: "Id",
              keyValue: 16L,
              columns: new[] { "ConfigKey", "CongigDisplayText" },
              values: new object[] { "Industry", "Industry" });

            migrationBuilder.UpdateData(
              table: "ConfigurationGroups",
              keyColumn: "Id",
              keyValue: 17L,
              columns: new[] { "ConfigKey", "CongigDisplayText" },
              values: new object[] { "Industry", "Industry" });

            migrationBuilder.UpdateData(
              table: "ConfigurationGroups",
              keyColumn: "Id",
              keyValue: 35L,
              columns: new[] { "ConfigKey", "CongigDisplayText" },
              values: new object[] { "Industry", "Industry" });

            migrationBuilder.UpdateData(
              table: "ConfigurationGroups",
              keyColumn: "Id",
              keyValue: 38L,
              columns: new[] { "ConfigKey", "CongigDisplayText" },
              values: new object[] { "Industry", "Industry" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
