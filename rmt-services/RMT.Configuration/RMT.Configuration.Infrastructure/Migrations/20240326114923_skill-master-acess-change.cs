using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillmasteracesschange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7350), new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7353) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7357), new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7358) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7361), new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7365), new DateTime(2024, 3, 26, 11, 49, 22, 446, DateTimeKind.Utc).AddTicks(7366) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 148L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 149L,
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
                values: new object[] { new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4171), new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4173) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4175), new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4175) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4177), new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4178) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4180), new DateTime(2024, 3, 19, 14, 19, 39, 82, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 148L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 149L,
                column: "IsActive",
                value: true);
        }
    }
}
