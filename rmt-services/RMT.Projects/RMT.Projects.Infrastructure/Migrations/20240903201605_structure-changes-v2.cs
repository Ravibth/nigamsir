using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechangesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BUId",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Offerings",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferingsId",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solutions",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolutionsId",
                table: "Projects",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Offerings",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OfferingsId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Solutions",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SolutionsId",
                table: "Projects");
        }
    }
}
