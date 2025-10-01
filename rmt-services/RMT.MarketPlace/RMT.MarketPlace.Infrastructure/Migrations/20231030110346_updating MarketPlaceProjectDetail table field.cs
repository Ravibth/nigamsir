using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingMarketPlaceProjectDetailtablefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ChargableType",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElForJob",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElForPipeLine",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expertise",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevenueUnit",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subindustry",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "businessUnit",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "csp",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "smeg",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargableType",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "ElForJob",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "ElForPipeLine",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Expertise",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "RevenueUnit",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Subindustry",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "businessUnit",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "csp",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "smeg",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
