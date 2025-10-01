using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newdb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferenceOrder",
                table: "EmployeePreferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferenceOrder",
                table: "EmployeePreferences");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3391), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3396) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3404), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3404) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3409), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3409) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3413), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3414) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3418), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3418) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3423), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3423) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3428), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3429) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3433), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3437), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3441), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3441) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3446), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3447) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3451), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3451) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3455), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3456) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3459), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3463), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3464) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3467), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3468) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3472), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3473) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3476), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3477) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3480), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3481) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3486), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3490), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3593), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3594) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3598), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3598) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3602), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3602) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3606), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3606) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3610), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3611) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3614), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3615) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3619), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3619) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3623), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3624) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3628), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3629) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3632), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3633) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3636), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3637) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3640), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3641) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3644), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3644) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3648), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3648) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3652), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3656), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3656) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3661), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3661) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3664), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3665) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3669), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3669) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3672), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3673) });
        }
    }
}
