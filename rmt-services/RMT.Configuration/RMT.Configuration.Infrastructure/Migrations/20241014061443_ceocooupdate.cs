using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ceocooupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleContextMenu",
                columns: new[] { "Id", "ContextMenuId", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 99L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 100L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 101L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 102L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 103L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 104L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 105L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 106L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 107L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 108L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 109L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 209L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 210L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 211L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 212L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 213L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 214L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 215L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 216L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 217L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 218L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 219L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 220L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 221L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 222L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 223L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 224L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 225L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 226L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 227L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 228L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 229L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" },
                    { 230L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "CEOCOO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 230L);
        }
    }
}
