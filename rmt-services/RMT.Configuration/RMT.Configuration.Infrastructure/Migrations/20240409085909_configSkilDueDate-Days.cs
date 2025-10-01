using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configSkilDueDateDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfigurationGroupMasters",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "CreatedAt", "CreatedBy", "DefaultValue", "IsActive", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[] { 77L, "Number_Of_Days_For_Skill_Approval_Duedate", "Number Of Days For Skill Approval Duedate", "Number_Of_Days_For_Skill_Approval_Duedate", "Number Of Days For Skill Approval Duedate", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "3", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigType", "ConfigurationGroupMasterId", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "SortOrder" },
                values: new object[,]
                {
                    { 78L, "3", "EXPERTISE", 77L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 660 },
                    { 79L, "3", "BUSINESS_UNIT", 77L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 650 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 77L);
        }
    }
}
