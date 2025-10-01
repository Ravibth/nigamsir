using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seederdatafieldformarketplace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FieldForMarketPlaces",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DisplayName", "InternalName", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6107), "System", "Name of Project", "projectName", true, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6111), "System" },
                    { 2L, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6165), "System", "Client Name", "clientName", true, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6166), "System" },
                    { 3L, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6168), "System", "Client Group", "clientGroup", true, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6168), "System" },
                    { 4L, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6169), "System", "Project ID", "projectCode", true, new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6170), "System" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
