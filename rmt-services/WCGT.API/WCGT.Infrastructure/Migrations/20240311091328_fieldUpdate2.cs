using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fieldUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatePerHour",
                table: "Budget");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "RateDesignationMaster",
                newName: "Designation_id");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Budget",
                newName: "Designation_id");

            migrationBuilder.AddColumn<string>(
                name: "grade",
                table: "Designations",
                type: "text",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "grade",
                table: "Designations");

            migrationBuilder.RenameColumn(
                name: "Designation_id",
                table: "RateDesignationMaster",
                newName: "Designation");

            migrationBuilder.RenameColumn(
                name: "Designation_id",
                table: "Budget",
                newName: "Designation");

            migrationBuilder.AddColumn<double>(
                name: "RatePerHour",
                table: "Budget",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
