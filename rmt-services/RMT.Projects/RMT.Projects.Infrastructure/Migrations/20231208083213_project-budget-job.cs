using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class projectbudgetjob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                  table: "ProjectBudget",
             type: "text",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "JobCode",
             table: "ProjectBudget");
        }
    }
}
