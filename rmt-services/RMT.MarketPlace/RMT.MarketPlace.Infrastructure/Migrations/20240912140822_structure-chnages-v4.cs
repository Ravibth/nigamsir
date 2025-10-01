using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechnagesv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterested",
                table: "EmpProjectInterestScore");

            migrationBuilder.DropColumn(
                name: "EmpMID",
                table: "EmpProjectInterest");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmpMID",
                table: "EmpProjectInterest",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterested",
                table: "EmpProjectInterestScore",
                type: "boolean",
                nullable: true);
        }
    }
}
