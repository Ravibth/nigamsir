using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingfieldintable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarketClosed",
                table: "Projects",
                newName: "IsRequisitionCreationallowed");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublishedToMarketPlace",
                table: "Projects",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublishedToMarketPlace",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "IsRequisitionCreationallowed",
                table: "Projects",
                newName: "MarketClosed");
        }
    }
}
