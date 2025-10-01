using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillreviewmenuadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "ConfigType", "CongigDisplayText", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 70L, "10", "Requisition_form", "Requisition form", "Skills", "BUSINESS_UNIT", "Skills", new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1691), "System", true, true, new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1693), "System", "INTEGER" },
                    { 71L, "10", "Requisition_form", "Requisition form", "Skills", "EXPERTISE", "Skills", new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1695), "System", true, true, new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1696), "System", "INTEGER" },
                    { 72L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "BUSINESS_UNIT", "Skills", new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1697), "System", true, true, new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1698), "System", "INTEGER" },
                    { 73L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "EXPERTISE", "Skills", new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1699), "System", true, true, new DateTime(2024, 3, 3, 15, 39, 44, 965, DateTimeKind.Utc).AddTicks(1700), "System", "INTEGER" }
                });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "IsDisplay", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[] { 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Skills Review", "Skills Review", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 93, "", "/skill-review" });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 127L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 128L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 129L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 130L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 131L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 132L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 133L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 17L);
        }
    }
}
