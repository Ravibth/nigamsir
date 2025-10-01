using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmpName",
                table: "EmpProjectInterestScore",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterested",
                table: "EmpProjectInterestScore",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpName",
                table: "EmpProjectInterestScore");

            migrationBuilder.DropColumn(
                name: "IsInterested",
                table: "EmpProjectInterestScore");
        }
    }
}
