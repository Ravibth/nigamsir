using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class applicationlevelsettingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationLevelSettings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "ModifiedAt", "ModifiedBy", "Value" },
                values: new object[] { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, "ResignedUserAvailabilityThreshold", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationLevelSettings",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
