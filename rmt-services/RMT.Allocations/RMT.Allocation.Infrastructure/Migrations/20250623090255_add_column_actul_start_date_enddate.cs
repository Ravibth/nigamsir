using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_column_actul_start_date_enddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ActualEndDate",
                table: "PublishedResAllocDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ActualStartDate",
                table: "PublishedResAllocDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualEndDate",
                table: "PublishedResAllocDetails");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                table: "PublishedResAllocDetails");
        }
    }
}
