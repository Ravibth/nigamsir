using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class publishedDetailsTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "PublishedResAllocDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "PublishedResAllocDetails");
        }
    }
}
