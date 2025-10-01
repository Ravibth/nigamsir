using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcontextmenureviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: true );

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: true);
            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40,
                column: "IsActive",
                value: true);
            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41,
                column: "IsActive",
                value: true);
            migrationBuilder.UpdateData(
              table: "RoleContextMenu",
              keyColumn: "Id",
              keyValue: 42,
              column: "IsActive",
              value: true);
            migrationBuilder.UpdateData(
              table: "RoleContextMenu",
              keyColumn: "Id",
              keyValue: 81,
              column: "IsActive",
              value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41,
                column: "IsActive",
                value: false);
            migrationBuilder.UpdateData(
              table: "RoleContextMenu",
              keyColumn: "Id",
              keyValue: 42,
              column: "IsActive",
              value: false);
            migrationBuilder.UpdateData(
              table: "RoleContextMenu",
              keyColumn: "Id",
              keyValue: 81,
              column: "IsActive",
              value: false);
        }
    }
}
