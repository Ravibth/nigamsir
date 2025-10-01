using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_menu_changes_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8536), new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8538) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8540), new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8541) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8543), new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8543) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8545), new DateTime(2024, 3, 20, 10, 9, 15, 269, DateTimeKind.Utc).AddTicks(8546) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                column: "IsDisplay",
                value: false);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                column: "IsDisplay",
                value: false);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Order",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3556), new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3559) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3561), new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3562) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3564), new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3564) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3566), new DateTime(2024, 3, 20, 9, 59, 3, 42, DateTimeKind.Utc).AddTicks(3566) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Order",
                value: 50);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Order",
                value: 60);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Order",
                value: 70);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                column: "IsDisplay",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                column: "IsDisplay",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Order",
                value: 91);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Order",
                value: 92);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Order",
                value: 93);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Order",
                value: 94);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Order",
                value: 4);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Order",
                value: 4);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Order",
                value: 91);
        }
    }
}
