using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configdataseeder2310 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("0a3d27e7-2208-47fa-90e4-ae5deaf57edd"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("22a77cc1-8e19-4a90-9a01-fe6c93d297a7"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("9c7105a7-e371-43d2-b957-a9c4be1abd7b"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("b1883bbb-1188-48b7-96b4-c7ffb4b1e396"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(100|[1-9][0-9]|[1-9])$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("b09dbb85-e0e2-4a5e-ae2c-c49542e407ee"), new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"), new DateTime(2024, 10, 23, 7, 56, 55, 842, DateTimeKind.Utc).AddTicks(9286), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"Competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"Offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"Solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"0\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"0\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 55, 842, DateTimeKind.Utc).AddTicks(9649), "system" },
                    { new Guid("efebdaad-4195-4bf6-a0a3-1f60ab537d24"), new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"), new DateTime(2024, 10, 23, 7, 56, 55, 845, DateTimeKind.Utc).AddTicks(8090), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 55, 845, DateTimeKind.Utc).AddTicks(8093), "system" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b09dbb85-e0e2-4a5e-ae2c-c49542e407ee"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("efebdaad-4195-4bf6-a0a3-1f60ab537d24"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("9c7105a7-e371-43d2-b957-a9c4be1abd7b"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(100|[1-9][0-9]|[1-9])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("b1883bbb-1188-48b7-96b4-c7ffb4b1e396"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("0a3d27e7-2208-47fa-90e4-ae5deaf57edd"), new Guid("9c7105a7-e371-43d2-b957-a9c4be1abd7b"), new DateTime(2024, 10, 23, 7, 56, 3, 637, DateTimeKind.Utc).AddTicks(3545), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 3, 637, DateTimeKind.Utc).AddTicks(3547), "system" },
                    { new Guid("22a77cc1-8e19-4a90-9a01-fe6c93d297a7"), new Guid("b1883bbb-1188-48b7-96b4-c7ffb4b1e396"), new DateTime(2024, 10, 23, 7, 56, 3, 635, DateTimeKind.Utc).AddTicks(3414), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"Competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"Offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"Solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"0\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"0\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 3, 635, DateTimeKind.Utc).AddTicks(3989), "system" }
                });
        }
    }
}
