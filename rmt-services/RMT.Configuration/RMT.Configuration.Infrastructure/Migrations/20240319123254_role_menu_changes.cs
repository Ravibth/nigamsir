using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_menu_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SMEGMaster",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SMEGMaster",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchedularStartTime",
                table: "SchedularLog",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchedularEndTime",
                table: "SchedularLog",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SchedularLog",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchedularLog",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RUMaster",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RUMaster",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleMenu",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleMenu",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleContextMenu",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleContextMenu",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProjectConfigurations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectConfigurations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "MenuMaster",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MenuMaster",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ExpertiesMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExpertiesMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ContextMenuMaster",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContextMenuMaster",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ConfigurationGroups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ConfigurationGroups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "BusinessUnitMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusinessUnitMasters",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Bu_Experties_Grps",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bu_Experties_Grps",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ApplicationLevelSettings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationLevelSettings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5785), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5787) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5790), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5792), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5793) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5795), new DateTime(2024, 3, 19, 12, 32, 53, 256, DateTimeKind.Utc).AddTicks(5796) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ParentId",
                value: "22");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ParentId",
                value: "22");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Path",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Path",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L,
                column: "ParentId",
                value: "15");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 17L,
                column: "ParentId",
                value: "15");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 18L,
                column: "ParentId",
                value: "15");

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "IsDisplay", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[,]
                {
                    { 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "My Preferences", "My Preferences", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 4, "10", "/my-preference" },
                    { 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "My Calender", "My Calender", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 4, "10", "/my-calender" },
                    { 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Skill Master", "Skill Master", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 91, "15", "/skillmaster" },
                    { 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Admin Settings", "Admin Settings", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 100, "", "" }
                });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                column: "IsActive",
                value: true);

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 181L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" },
                    { 182L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 143L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 144L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" },
                    { 145L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalEl" },
                    { 146L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalDelegate" },
                    { 147L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 148L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 149L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 151L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 152L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Reviewer" },
                    { 153L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 154L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" },
                    { 155L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalEl" },
                    { 156L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalDelegate" },
                    { 157L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 158L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 159L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 160L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 161L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Reviewer" },
                    { 162L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 163L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" },
                    { 164L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalEl" },
                    { 165L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalDelegate" },
                    { 166L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 167L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 168L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 169L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 171L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Reviewer" },
                    { 172L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 173L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" },
                    { 174L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalEl" },
                    { 175L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalDelegate" },
                    { 176L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 177L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 178L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 179L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 180L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SMEGMaster",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SMEGMaster",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchedularStartTime",
                table: "SchedularLog",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SchedularEndTime",
                table: "SchedularLog",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SchedularLog",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchedularLog",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RUMaster",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RUMaster",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleMenu",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleMenu",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleContextMenu",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleContextMenu",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProjectConfigurations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectConfigurations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "MenuMaster",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MenuMaster",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ExpertiesMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExpertiesMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ContextMenuMaster",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContextMenuMaster",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ConfigurationGroups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ConfigurationGroups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "BusinessUnitMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusinessUnitMasters",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Bu_Experties_Grps",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bu_Experties_Grps",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ApplicationLevelSettings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationLevelSettings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1908), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1911) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1914), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1915) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1918), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1919) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1922), new DateTime(2024, 3, 15, 18, 50, 51, 671, DateTimeKind.Utc).AddTicks(1923) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ParentId",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ParentId",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Path",
                value: "/manage");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Path",
                value: "/skillmaster");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L,
                column: "ParentId",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 17L,
                column: "ParentId",
                value: "");

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 18L,
                column: "ParentId",
                value: "");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                column: "IsActive",
                value: false);
        }
    }
}
