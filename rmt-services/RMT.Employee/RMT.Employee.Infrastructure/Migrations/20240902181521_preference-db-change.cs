using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class preferencedbchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "PreferenceMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PreferenceMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "EmployeePreferences",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EmployeePreferences",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "PreferenceInfo",
                table: "EmployeePreferences",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7738), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7754), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7756) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7763), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7764) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7771), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7772) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7779), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7786), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7788) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7795), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7803), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7810), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7812) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7818), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7819) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7983), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7985) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7993), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(7995) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8001), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8003) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8010), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8017), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8019) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8025), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8026) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8036), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8038) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8044), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8046) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8052), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8053) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8063), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8065) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8071), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8072) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8079), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8081) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8087), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8088) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8095), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8096) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8102), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8104) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8109), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8111) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8117), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8119) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8125), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8126) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8132), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8134) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8141), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8143) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8149), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8150) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8156), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8158) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8164), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8172), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8173) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8180), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8181) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8187), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8189) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8195), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8202), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8210), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8211) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8217), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8219) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8224), new DateTime(2024, 9, 2, 18, 15, 21, 155, DateTimeKind.Utc).AddTicks(8226) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferenceInfo",
                table: "EmployeePreferences");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "PreferenceMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PreferenceMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "EmployeePreferences",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EmployeePreferences",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(776), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(787), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(788) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(792), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(793) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(797), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(798) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(801), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(802) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(805), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(806) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(810), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(811) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(815), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(816) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(820), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(821) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(825), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(825) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(830), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(835), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(835) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(839), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(840) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(843), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(844) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(848), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(849) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(853), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(854) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(857), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(862), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(862) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(866), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(866) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(871), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(872) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(875), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(876) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(881), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(882) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(886), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(886) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(890), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(891) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(894), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(895) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(898), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(899) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(903), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(904) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(908), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(909) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(913), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(913) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(917), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(918) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(921), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(922) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(926), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(926) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(930), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(931) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(935), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(936) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(940), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(941) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(944), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(945) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(948), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(949) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(953), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(953) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(957), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(957) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(961), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(962) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(965), new DateTime(2023, 12, 27, 14, 28, 21, 699, DateTimeKind.Utc).AddTicks(966) });
        }
    }
}
