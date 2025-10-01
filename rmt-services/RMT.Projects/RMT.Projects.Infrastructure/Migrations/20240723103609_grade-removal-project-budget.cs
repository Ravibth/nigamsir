using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class graderemovalprojectbudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "ProjectBudget");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "ProjectBudget",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
