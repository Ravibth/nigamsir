using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillSearchandOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 84L);

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

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "ConfigType", "CongigDisplayText", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 74L, "true", "Permission_for_Additional_EL", "Permission for Additional EL", "Permission_for_Additional_EL", "EXPERTISE", "Permission for Additional EL", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" },
                    { 75L, "true", "Permission_for_Additional_EL", "Permission for Additional EL", "Permission_for_Additional_EL", "BUSINESS_UNIT", "Permission for Additional EL", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" },
                    { 76L, "true", "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Permission_for_Additional_Delegate", "EXPERTISE", "Permission for Additional Delegate", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" },
                    { 77L, "true", "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Permission_for_Additional_Delegate", "BUSINESS_UNIT", "Permission for Additional Delegate", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" }
                });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "IsDisplay", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[] { 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Skill Search", "Skill Search", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 94, "", "/searchskill" });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 36L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 45L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 48L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 71L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Role",
                value: "ResourceRequestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 45L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 48L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 59L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 60L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 61L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 66L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 68L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 69L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 71L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 73L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 75L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 76L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 80L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 82L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 83L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 84L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 99L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 100L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 101L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 102L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 103L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 104L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 105L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 106L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 107L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 108L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 109L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 110L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 111L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 112L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 113L,
                columns: new[] { "IsActive", "Role" },
                values: new object[] { true, "Leaders" });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 114L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                columns: new[] { "IsActive", "Role" },
                values: new object[] { false, "Reviewer" });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 120L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 122L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 123L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 124L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 127L,
                column: "Role",
                value: "Leaders");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 129L,
                column: "Role",
                value: "AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 130L,
                column: "Role",
                value: "AdditionalEl");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 131L,
                column: "Role",
                value: "Reviewer");

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 142L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 134L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" },
                    { 135L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 136L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalDelegate" },
                    { 137L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "AdditionalEl" },
                    { 138L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Reviewer" },
                    { 139L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 140L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 141L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "ResourceRequestor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1691), new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1693) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1695), new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1696) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1697), new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1698) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1699), new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 36L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 45L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 48L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 71L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.InsertData(
                table: "RoleContextMenu",
                columns: new[] { "Id", "ContextMenuId", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[] { 84L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Role",
                value: "Resource Requestor");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 45L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 48L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 54L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 59L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 60L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 61L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 66L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 68L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 69L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 71L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 73L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 75L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 76L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 80L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 82L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 83L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 84L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 99L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 100L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 101L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 102L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 103L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 104L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 105L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 106L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 107L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 108L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 109L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 110L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 111L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 112L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 113L,
                columns: new[] { "IsActive", "Role" },
                values: new object[] { false, "Leader" });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 114L,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                columns: new[] { "IsActive", "Role" },
                values: new object[] { true, "Request Reviewer" });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 120L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 122L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 123L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 124L,
                column: "Role",
                value: "Request Reviewer");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 127L,
                column: "Role",
                value: "Leader");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 129L,
                column: "Role",
                value: "Additional Delegate");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 130L,
                column: "Role",
                value: "Additional EL");

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 131L,
                column: "Role",
                value: "Request Reviewer");
        }
    }
}
