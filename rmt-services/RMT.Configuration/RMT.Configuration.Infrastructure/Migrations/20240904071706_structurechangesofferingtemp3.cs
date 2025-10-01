using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechangesofferingtemp3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1010L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1012L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1025L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1031L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1032L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1043L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 41L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfigurationGroupMasters",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "CreatedAt", "CreatedBy", "DefaultValue", "IsActive", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 10L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 12L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 24L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 28L, "Requisition_form", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 29L, "Requisition_form", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 31L, "Requisition_form", "Requisition form", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 32L, "Requisition_form", "Requisition form", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 41L, "Requisition_form", "Requisition form", "business_unit", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "8", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigType", "ConfigurationGroupMasterId", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "SortOrder" },
                values: new object[,]
                {
                    { 10L, "8", "EXPERTISE", 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 500 },
                    { 11L, "8", "BUSINESS_UNIT", 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 490 },
                    { 12L, "6", "EXPERTISE", 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 620 },
                    { 13L, "6", "BUSINESS_UNIT", 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 610 },
                    { 24L, "6", "BUSINESS_UNIT", 24L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 470 },
                    { 25L, "9", "EXPERTISE", 24L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 480 },
                    { 28L, "8", "BUSINESS_UNIT", 28L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 230 },
                    { 29L, "6", "BUSINESS_UNIT", 29L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 370 },
                    { 31L, "8", "EXPERTISE", 31L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 240 },
                    { 32L, "6", "EXPERTISE", 32L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 380 },
                    { 41L, "6", "BUSINESS_UNIT", 41L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 210 },
                    { 43L, "9", "EXPERTISE", 41L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 220 },
                    { 1010L, "8", "OFFERINGS", 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 500 },
                    { 1012L, "6", "OFFERINGS", 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 620 },
                    { 1025L, "9", "OFFERINGS", 24L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 480 },
                    { 1031L, "8", "OFFERINGS", 31L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 240 },
                    { 1032L, "6", "OFFERINGS", 32L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 380 },
                    { 1043L, "9", "OFFERINGS", 41L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 220 }
                });
        }
    }
}
