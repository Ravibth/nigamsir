using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingMarketPlaceProjectDetailtablefieldagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "smeg",
                table: "MarketPlaceProjectDetail",
                newName: "Smeg");

            migrationBuilder.RenameColumn(
                name: "csp",
                table: "MarketPlaceProjectDetail",
                newName: "Csp");

            migrationBuilder.RenameColumn(
                name: "businessUnit",
                table: "MarketPlaceProjectDetail",
                newName: "BusinessUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Smeg",
                table: "MarketPlaceProjectDetail",
                newName: "smeg");

            migrationBuilder.RenameColumn(
                name: "Csp",
                table: "MarketPlaceProjectDetail",
                newName: "csp");

            migrationBuilder.RenameColumn(
                name: "BusinessUnit",
                table: "MarketPlaceProjectDetail",
                newName: "businessUnit");
        }
    }
}
