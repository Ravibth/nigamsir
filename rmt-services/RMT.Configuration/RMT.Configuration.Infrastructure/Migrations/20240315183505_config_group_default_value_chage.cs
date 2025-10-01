using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class config_group_default_value_chage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "AllValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L,
                column: "AllValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L,
                column: "AllValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L,
                column: "AllValue",
                value: "1");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L,
                column: "AllValue",
                value: "1");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "AllValue",
                value: "{\"activationStatus\":\"false\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L,
                column: "AllValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"2\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L,
                column: "AllValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"2\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L,
                column: "AllValue",
                value: "5");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L,
                column: "AllValue",
                value: "5");

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
        }
    }
}
