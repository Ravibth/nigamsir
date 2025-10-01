using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leadersroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "IsActive",
                value: false);

            migrationBuilder.InsertData(
                table: "RoleContextMenu",
                columns: new[] { "Id", "ContextMenuId", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 84L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 85L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 86L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 87L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "IsActive",
                value: true);
        }
    }
}
