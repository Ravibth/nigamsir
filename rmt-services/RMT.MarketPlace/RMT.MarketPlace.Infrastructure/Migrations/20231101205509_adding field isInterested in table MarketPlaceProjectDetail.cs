using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingfieldisInterestedintableMarketPlaceProjectDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterested",
                table: "MarketPlaceProjectDetail",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterested",
                table: "MarketPlaceProjectDetail");
        }
    }
}
