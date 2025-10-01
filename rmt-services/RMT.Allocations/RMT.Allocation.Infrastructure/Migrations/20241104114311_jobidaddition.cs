using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jobidaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "UnPublishedResAllocDays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "PublishedResAllocDays",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobId",
                table: "UnPublishedResAllocDays");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "PublishedResAllocDays");
        }
    }
}
