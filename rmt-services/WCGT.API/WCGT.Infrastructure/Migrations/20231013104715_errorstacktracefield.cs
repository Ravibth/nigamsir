using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class errorstacktracefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "error_stacktrace",
                table: "WCGTDataLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_service_partner_id",
                table: "Pipelines",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "error_stacktrace",
                table: "WCGTDataLogs");

            migrationBuilder.DropColumn(
                name: "client_service_partner_id",
                table: "Pipelines");
        }
    }
}
