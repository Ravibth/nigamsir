using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechangesofferingtemp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfigurationGroupMasters",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "CreatedAt", "CreatedBy", "DefaultValue", "IsActive", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 78L, "Requisition_form", "Requisition form", "offerings", "Offerings", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 79L, "Requisition_form", "Requisition form", "solutions", "Solutions", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 80L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "offerings", "Offerings", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 81L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "solutions", "Solutions", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigType", "ConfigurationGroupMasterId", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "SortOrder" },
                values: new object[,]
                {
                    { 82L, "9", "EXPERTISE", 78L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 230 },
                    { 83L, "9", "BUSINESS_UNIT", 78L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 240 },
                    { 84L, "9", "EXPERTISE", 79L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 230 },
                    { 85L, "9", "BUSINESS_UNIT", 79L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 240 },
                    { 86L, "9", "BUSINESS_UNIT", 80L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 },
                    { 87L, "9", "BUSINESS_UNIT", 80L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 },
                    { 88L, "9", "BUSINESS_UNIT", 81L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 },
                    { 89L, "9", "BUSINESS_UNIT", 81L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 },
                    { 1082L, "9", "OFFERINGS", 78L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 230 },
                    { 1084L, "9", "OFFERINGS", 79L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 230 },
                    { 1086L, "9", "OFFERINGS", 80L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 },
                    { 1088L, "9", "OFFERINGS", 81L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 460 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1082L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1084L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1086L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1088L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 81L);
        }
    }
}
