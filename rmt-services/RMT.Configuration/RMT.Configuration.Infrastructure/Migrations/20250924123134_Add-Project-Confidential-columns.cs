using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectConfidentialcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                  table: "ApplicationLevelSettings",
                  columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "ModifiedAt", "ModifiedBy", "Value" },
                  values: new object[] { 3L, new DateTime(2025, 09, 24, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, "ProjectMaskingColumn", new DateTime(2025, 09, 24, 18, 30, 0, 0, DateTimeKind.Utc), "System", "JobName,ClientName,ClientGroup,Description" });

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "ApplicationLevelSettings",
               keyColumn: "Id",
               keyValue: 3L);
        }
    }
}
