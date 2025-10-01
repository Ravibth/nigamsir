using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_data_filling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fun_resource_timesheet_view",
                table: "fun_resource_timesheet_view");

            migrationBuilder.RenameTable(
                name: "fun_resource_timesheet_view",
                newName: "sp_resource_timesheet_view");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sp_resource_timesheet_view",
                table: "sp_resource_timesheet_view",
                column: "empcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sp_resource_timesheet_view",
                table: "sp_resource_timesheet_view");

            migrationBuilder.RenameTable(
                name: "sp_resource_timesheet_view",
                newName: "fun_resource_timesheet_view");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fun_resource_timesheet_view",
                table: "fun_resource_timesheet_view",
                column: "empcode");
        }
    }
}
