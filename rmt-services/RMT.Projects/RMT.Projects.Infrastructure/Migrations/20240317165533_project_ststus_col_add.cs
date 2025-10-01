using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class project_ststus_col_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProjectActivationStatus",
                table: "Projects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectClosureState",
                table: "Projects",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectActivationStatus",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectClosureState",
                table: "Projects");
        }
    }
}
