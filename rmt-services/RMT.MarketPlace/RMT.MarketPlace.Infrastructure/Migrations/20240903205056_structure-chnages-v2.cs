using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechnagesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BUId",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Offerings",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferingsId",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solutions",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolutionsId",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUId",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Offerings",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "OfferingsId",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Solutions",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "SolutionsId",
                table: "MarketPlaceProjectDetail");
        }
    }
}
