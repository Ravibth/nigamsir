using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PCandJcCodeChnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "EmpProjectInterest");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "EmpProjectInterest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "EmpProjectInterest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "EmpProjectInterest",
                type: "text",
                nullable: true);
        }
    }
}
