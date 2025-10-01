using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingMarketPlaceProjectDetailtablefieldwithProposedCSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProposedCsp",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProposedCsp",
                table: "MarketPlaceProjectDetail");
        }
    }
}
