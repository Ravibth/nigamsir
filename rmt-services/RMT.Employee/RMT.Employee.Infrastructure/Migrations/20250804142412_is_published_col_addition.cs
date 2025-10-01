using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class is_published_col_addition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_published",
                table: "EmployeeQualification",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_published",
                table: "EmployeeQualification");
        }
    }
}
