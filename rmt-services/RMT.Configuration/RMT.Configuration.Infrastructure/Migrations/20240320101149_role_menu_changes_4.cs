using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_menu_changes_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8514), new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8517) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8519), new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8519) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8522), new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8522) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8525), new DateTime(2024, 3, 20, 10, 11, 48, 770, DateTimeKind.Utc).AddTicks(8525) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 1L,
                column: "IsDisplay",
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
                keyValue: 1L,
                column: "IsDisplay",
                value: true);
        }
    }
}
