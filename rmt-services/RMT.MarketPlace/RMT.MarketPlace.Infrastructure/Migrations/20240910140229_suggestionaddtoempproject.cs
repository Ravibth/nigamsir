using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suggestionaddtoempproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Suggestion",
                table: "EmpProjectInterestScore",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suggestion",
                table: "EmpProjectInterestScore");
        }
    }
}
