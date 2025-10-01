using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class context_menu_el_dl_chage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1908), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1911) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1914), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1915) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1918), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1919) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1922), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1923) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "IsActive",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7853), new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7856) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7858), new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7858) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7860), new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7862), new DateTime(2024, 3, 15, 18, 35, 4, 317, DateTimeKind.Utc).AddTicks(7863) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "IsActive",
                value: true);
        }
    }
}
