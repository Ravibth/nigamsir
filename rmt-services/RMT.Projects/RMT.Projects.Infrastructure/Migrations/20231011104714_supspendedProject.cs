using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class supspendedProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "Projects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedModifyAt",
                table: "Projects",
                type: "timestamp with time zone",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SuspendedModifyAt",
                table: "Projects");
        }
    }
}
