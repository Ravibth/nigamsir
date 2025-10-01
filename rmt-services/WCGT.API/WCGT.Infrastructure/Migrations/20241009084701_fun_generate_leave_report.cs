using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fun_generate_leave_report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generate_leave_report",
                columns: table => new
                {
                    leave_date = table.Column<DateOnly>(type: "date", nullable: false),
                    employee_email = table.Column<string>(type: "text", nullable: false),
                    emp_mid = table.Column<string>(type: "text", nullable: false),
                    leave_hours = table.Column<int>(type: "integer", nullable: false),
                    leave_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "generate_leave_report");
        }
    }
}
