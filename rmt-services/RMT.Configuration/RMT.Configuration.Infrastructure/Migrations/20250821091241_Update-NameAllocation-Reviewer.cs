using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameAllocationReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39,
                column: "IsActive",
                value: true);

         }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
              table: "RoleContextMenu",
              keyColumn: "Id",
              keyValue: 40,
              column: "IsActive",
              value: false);
        }
    }
}
