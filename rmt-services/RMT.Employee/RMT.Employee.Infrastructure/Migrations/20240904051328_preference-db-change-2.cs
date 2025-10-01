using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class preferencedbchange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "EmployeePreferences");

            migrationBuilder.DropColumn(
                name: "PreferenceName",
                table: "EmployeePreferences");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3424), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3437), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3447), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3447) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3449), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3452), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3452) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3454), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3454) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3457), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3457) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3459), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3461), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3461) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3463), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3464) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3465), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3466) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3470), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3472), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3473) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3474), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3475) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3477), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3477) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3480), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3482), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3483) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3484), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3485) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3487), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3489), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3489) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3491), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3493), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3494) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3496), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3496) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3498), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3499) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3501), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3502) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3552), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3552) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3555), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3555) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3557), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3558) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3559), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3562), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3562) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3564), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3564) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3566), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3567) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3569), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3569) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3571), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3571) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3573), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3574) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3576), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3576) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3578), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3578) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3580), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3581) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3583), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3583) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3586), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3586) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3588), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3588) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferenceId",
                table: "EmployeePreferences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferenceName",
                table: "EmployeePreferences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9736), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9753), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9755) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9761), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9763) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9769), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9777), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9778) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9784), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9786) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9793), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9794) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9800), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9802) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9810), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9815) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9830), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9832) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9839), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9840) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9848), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9919) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9926), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9928) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9934), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9936) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9943), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9944) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9982), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9984) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9991), new DateTime(2024, 9, 3, 6, 42, 28, 403, DateTimeKind.Utc).AddTicks(9992) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(3), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(4) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(13), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(15) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(23), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(24) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(32), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(34) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(44), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(54), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(55) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(64), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(66) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(73), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(75) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(83), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(85) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(94), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(96) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(104), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(106) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(114), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(115) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(124), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(126) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(134), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(136) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(144), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(146) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(153), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(155) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(163), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(164) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(172), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(174) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(181), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(183) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(191), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(193) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(200), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(202) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(209), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(211) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(218), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(220) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(227), new DateTime(2024, 9, 3, 6, 42, 28, 404, DateTimeKind.Utc).AddTicks(228) });
        }
    }
}
