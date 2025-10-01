using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_menu_changes_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9173), new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9176) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9178), new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9179) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9181), new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9182) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9187), new DateTime(2024, 3, 19, 13, 0, 53, 650, DateTimeKind.Utc).AddTicks(9188) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 139L,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5785), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5787) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5790), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5792), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5793) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5795), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5796) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 139L,
                column: "IsActive",
                value: false);
        }
    }
}
