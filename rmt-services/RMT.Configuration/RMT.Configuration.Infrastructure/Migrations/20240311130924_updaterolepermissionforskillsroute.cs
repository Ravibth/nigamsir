using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterolepermissionforskillsroute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(703), new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(705) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(707), new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(707) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(709), new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(712), new DateTime(2024, 3, 11, 13, 9, 22, 805, DateTimeKind.Utc).AddTicks(712) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 127L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 128L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 129L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 130L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 131L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 132L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 133L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 139L,
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
                values: new object[] { new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5489), new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5491) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5493), new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5493) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5495), new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5496) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5497), new DateTime(2024, 3, 6, 13, 30, 56, 38, DateTimeKind.Utc).AddTicks(5498) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 127L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 128L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 129L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 130L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 131L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 132L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 133L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 139L,
                column: "IsActive",
                value: true);
        }
    }
}
